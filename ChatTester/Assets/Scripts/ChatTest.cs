using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
//using ChatPacketStruct;
using RankPacketStruct;

public class ChatTest : MonoBehaviour
{
    //private const string ChatServerUrl = "ws://localhost:8080";
    private const string RankServerUrl = "ws://localhost:9090";
    private WebSocket webSocket;

    // Start is called before the first frame update
    private async void Start()
    {
        Debug.Log("ChatTest Start!");
        webSocket = new WebSocket(RankServerUrl);

        webSocket.OnOpen += OnOpen;
        webSocket.OnError += OnError;
        webSocket.OnClose += OnClose;

        // 기본 응답 받은 메시지 표기
        //webSocket.OnMessage += (bytes) => 
        //{
        //    Debug.Log("OnMessage!");
        //    Debug.Log(bytes);

        //    // getting the message as a string
        //    var message = System.Text.Encoding.UTF8.GetString(bytes);
        //    Debug.Log("OnMessage! " + message);
        //};

        webSocket.OnReceiveJson<ResponseBodyGetRankList>(nameof(ResponseGetRankList), OnReceiveBodyGetRankList);

        // Keep sending messages at every 0.3s
        // InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

        await webSocket.Connect();
    }

    public async void Send()
    {
        await webSocket.SendJson(new RequestGetRankList()
        {
            body = new RequestBodyGetRankList()
            {
                userUid = "useruid1234566",
                seasonId = 1,
            }
        });

    }

    // 기본 패킷 보내기
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

    // 서버에서 보내주는 값 패킷별 구분 확인
    private void OnReceiveBodyGetRankList(ResponseBodyGetRankList body)
    {
        Debug.Log($"OnReceiveBodyGetRankList : {body.error}");
        Debug.Log($"OnReceiveBodyGetRankList : {body.seasonRankList}");
    }

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
