using UnityEngine;
using System;
using NativeWebSocket;
using ChatPacket;

public class ChatTest : MonoBehaviour
{
    private const string ChatServerUrl = "ws://localhost:4000";
    private WebSocket ws;

    private async void Start()
    {
        Debug.Log("ChatTest Start!");

        ws = new WebSocket(ChatServerUrl);

        ws.OnOpen += OnOpen;
        ws.OnError += OnError;
        ws.OnClose += OnClose;

        ws.OnMessage += (bytes) =>
        {
            Debug.Log("OnMessage!");

            // getting the message as a string
            var message = System.Text.Encoding.UTF8.GetString(bytes); 
            Debug.Log("msg::" + message); // 서버에서 보내는 ping...옴

            // TODO: 정해진 방식대로 패킷을 받고 이 부분에서 분해 후 매핑된 함수 call
            // 패킷을 클라 쪽에서 어떤식으로 받아서 처리할지는 고민..그냥 솔루션 사용이 낫나.
        };

        await ws.Connect();
        // webSocket.OnReceiveChatJson<ResponseBodyChatLog>(nameof(ResponseChatLog), OnReceiveBodyChatLog);
    }

    public async void Send()
    {
        //await webSocket.SendChatJson(new RequestChatLog()
        //{
        //    body = new RequestBodyChatLog()
        //    {
        //        userUid = "useruid1234566",
        //    }
        //});
    }

    // 서버에서 보내주는 값 패킷별 구분 확인
    //private void OnReceiveBodyChatLog(ResponseBodyChatLog body)
    //{
    //    Debug.Log($"result errorcode: {body.error}");
    //}

    private void OnOpen()
    {
        Debug.Log("Connection open!");
    }

    private void OnError(string errorMessage)
    {
        Debug.Log("Connection error!");
    }

    private void OnClose(WebSocketCloseCode closeCode)
    {
        Debug.Log($"Connection closed : {closeCode}");
    }


    protected async void OnApplicationQuit()
    {
        await ws.Close();
    }

    private void Update()
    {
        //Debug.Log("ChatTest Update!");
        ws.DispatchMessageQueue();
    }
}
