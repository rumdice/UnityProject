using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;

public class ChatTest : MonoBehaviour
{
    private const string ChatServerUrl = "ws://localhost:8080";
    private WebSocket webSocket;

    // Start is called before the first frame update
    private async void Start()
    {
        Debug.Log("ChatTest Start!");
        webSocket = new WebSocket(ChatServerUrl);

        webSocket.OnOpen += OnOpen;
        webSocket.OnError += OnError;
        webSocket.OnClose += OnClose;
        
        // C# 화살표 메서드 오
        webSocket.OnMessage += (bytes) => 
        {
            Debug.Log("OnMessage!");
            Debug.Log(bytes);

            // getting the message as a string
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("OnMessage! " + message);
        };

        // Keep sending messages at every 0.3s
        // InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

        await webSocket.Connect();
    }

    public async void Send()
    {
        
    }

    /*
    private void OnMessage(byte[] bytes) 
    {
        Debug.Log("OnMessage!");
        Debug.Log(bytes);

        // getting the message as a string
        var message = System.Text.Encoding.UTF8.GetString(bytes);
        Debug.Log("OnMessage! " + message);
    }
    */

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
        await webSocket.Close();
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("ChatTest Update!");
        webSocket.DispatchMessageQueue();
    }
}
