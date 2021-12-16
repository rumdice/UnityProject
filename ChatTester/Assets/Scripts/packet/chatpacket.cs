using Newtonsoft.Json;
using System;
using static ErrorCode;


namespace ChatPacket
{
    public abstract class BaseRequest
    {
        [JsonIgnore]
        public virtual string URL => "";
        public string userUid;
    }
    public class BaseResponse
    {
        public ErrorCode error = ErrorCode.Undefinded;
        public string message;
    }


    public class RequestChatLogin : BaseRequest
    {
        public override string URL => "/ChatLogin";
        public string userName;
    }
    public class ResponseChatLogin : BaseResponse
    {
    }


    public class RequestNormalChat : BaseRequest
    {
        public string userUid;
        public string sendMessage;
    }
    public class RequestBodyChat : BaseResponse
    {
        public string userUid;
        public string recvMessage;
    }
   
}