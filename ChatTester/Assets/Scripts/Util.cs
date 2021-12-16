using NativeWebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RankPacketStruct;
using ChatPacket;

// TODO: 패킷 구조 변경 적용하기
// 임시코드 : 

public static class Util
{
    public static void OnReceiveRankJson<T>(this WebSocket webSocket, string header, Action<T> onReceiveCallback)
        where T : RankPacketStruct.ResponseBody
    {
        webSocket.OnMessage += (bytes) =>
        {
            var message = Encoding.UTF8.GetString(bytes);

            try
            {
                var recvJson = JsonConvert.DeserializeObject<RankPacketStruct.ResponsePacket<T>>(message);
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

    public static Task SendRankJson<T>(this WebSocket webSocket, T requestPacket) where T : RankPacketStruct.RequestPacketBase
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

    //public static Task SendChatJson<T>(this WebSocket webSocket, T requestPacket) where T : ChatPacketStruct.RequestPacketBase
    //{
    //    if (webSocket != null && webSocket.State == WebSocketState.Open)
    //    {
    //        var json = JsonConvert.SerializeObject(requestPacket);
    //        Debug.Log($"[web socket] send json : {json}");
    //        return webSocket.SendText(json);
    //    }
    //    else
    //    {
    //        Debug.Log("WebSocket is not connected.");
    //        return null;
    //    }
    //}

    //public static void OnReceiveChatJson<T>(this WebSocket webSocket, string header, Action<T> onReceiveCallback)
    //   where T : ChatPacketStruct.ResponseBody
    //{
    //    webSocket.OnMessage += (bytes) =>
    //    {
    //        var message = Encoding.UTF8.GetString(bytes);

    //        try
    //        {
    //            var recvJson = JsonConvert.DeserializeObject<ChatPacketStruct.ResponsePacket<T>>(message);
    //            if (recvJson.header == header)
    //            {
    //                Debug.Log($"[web socket] receive ({recvJson.header}) : {JsonUtility.ToJson(recvJson.body)}");
    //                onReceiveCallback?.Invoke(recvJson.body);
    //            }
    //        }
    //        catch (JsonSerializationException ex)
    //        {
    //            Debug.Log($"Json Parse Error : {ex.Message}");
    //        }
    //        catch (Exception)
    //        {
    //            Debug.Log($"OnMessage : {message}");
    //        }
    //    };
    //}
}
