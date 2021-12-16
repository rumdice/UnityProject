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
            Debug.Log("msg::" + message); // �������� ������ ping...��

            // TODO: ������ ��Ĵ�� ��Ŷ�� �ް� �� �κп��� ���� �� ���ε� �Լ� call
            // ��Ŷ�� Ŭ�� �ʿ��� ������� �޾Ƽ� ó�������� ���..�׳� �ַ�� ����� ����.
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

    // �������� �����ִ� �� ��Ŷ�� ���� Ȯ��
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
