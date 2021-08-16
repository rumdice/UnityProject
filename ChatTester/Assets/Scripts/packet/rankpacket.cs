using CommonPacketStruct;
using Newtonsoft.Json;
using System;

namespace RankPacketStruct
{
    // Request Packet Body
    public class RequestBodyUpdateRankScore : RequestBody
    {
        public string userUid;
        public int seasonId;
        public int score;
    }
    public class RequestBodyGetRankList : RequestBody
    {
        public string userUid;
        public int seasonId;
    }

    // Response Packet Body
    public class ResponseBodyUpdateRankScore : ResponseBody
    {
        public string userUid; // echo response (확인용)
    }
    public class ResponseBodyGetRankList : ResponseBody
    {
        public RankSeasonData[] seasonRankList;
    }




    // Request Packet Full
    public class RequestUpdateRankScore : RequestPacket<RequestBodyUpdateRankScore>
    {
        public override string header => nameof(RequestUpdateRankScore);
    }
    public class RequestGetRankList : RequestPacket<RequestBodyGetRankList>
    {
        public override string header => nameof(RequestGetRankList);
    }

    // Response Packet Full
    public class ResponseUpdateRankScore : ResponsePacket<ResponseBodyUpdateRankScore> { }
    public class ResponseGetRankList : ResponsePacket<ResponseBodyGetRankList> { }





    //////////////////////////////////////////////////////////////////////////////
    // Data
    public class RankSeasonData
    {
        public int seasonId;
        public string userUid;
        public string userName;
        public int score;
    }




    //////////////////////////////////////////////////////////////////////////////
    public class RequestBody { }
    public class ResponseBody
    {
        // TODO: common.cs에 정의 되어 있음. 
        // 툴로 c# to ts 전환시 해당 부분의 import 가 되지는 않아서 생성된 ts파일에서 import를 걸어줘야 함. 
        // gRPC를 쓰면 해결 될 듯..
        public ErrorCode error = ErrorCode.Undefinded;
    }


    public class RequestPacketBase
    {
        public virtual string header => "";
    }
    public class ResponsePacketBase
    {
        public string header;
    }

    public class RequestPacket<T> : RequestPacketBase where T : RequestBody
    {
        public T body;
    }
    public class ResponsePacket<T> : ResponsePacketBase where T : ResponseBody
    {
        public T body;
    }
}