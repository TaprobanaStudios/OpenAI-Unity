using System;
using System.Runtime.Serialization;

namespace com.studios.taprobana
{
    [Serializable]
    public class OpenAiRequestException : Exception
    {
        public string Type { get; }
        public string Param { get; }
        public string Code { get; }

        public OpenAiRequestException(ErrorInfo errorInfo) : base(errorInfo?.Error?.Message)
        {
            this.Type = errorInfo?.Error?.Type;
            this.Param = errorInfo?.Error?.Param;
            this.Code = errorInfo?.Error?.Code;
        }

        protected OpenAiRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Type = info.GetString("Type");
            this.Param = info.GetString("Param");
            this.Code = info.GetString("Code");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Type", this.Type);
            info.AddValue("Param", this.Param);
            info.AddValue("Code", this.Code);
        }
    }
}
