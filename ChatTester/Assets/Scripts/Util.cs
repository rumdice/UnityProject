using NativeWebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RankPacketStruct;

public static class Util
{
    public static void OnReceiveJson<T>(this WebSocket webSocket, string header, Action<T> onReceiveCallback)
        where T : ResponseBody
    {
        webSocket.OnMessage += (bytes) =>
        {
            var message = Encoding.UTF8.GetString(bytes);

            try
            {
                var recvJson = JsonConvert.DeserializeObject<ResponsePacket<T>>(message);
                if (recvJson.header == header)
                {
                    Debug.Log($"[web socket] receive ({recvJson.header}) : {JsonUtility.ToJson(recvJson.body)}");
                    onReceiveCallback?.Invoke(recvJson.body);
                }
            }
            catch (JsonSerializationException ex)
            {
                Debug.Log($"Json Parse Error : {ex.Message}");
            }
            catch (Exception)
            {
                Debug.Log($"OnMessage : {message}");
            }
        };
    }

    public static Task SendJson<T>(this WebSocket webSocket, T requestPacket) where T : RequestPacketBase
    {
        if (webSocket != null && webSocket.State == WebSocketState.Open)
        {
            var json = JsonConvert.SerializeObject(requestPacket);
            Debug.Log($"[web socket] send json : {json}");
            return webSocket.SendText(json);
        }
        else
        {
            Debug.Log("WebSocket is not connected.");
            return null;
        }
    }
}
