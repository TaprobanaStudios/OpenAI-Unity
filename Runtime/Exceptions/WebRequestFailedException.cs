using System;

namespace com.studios.taprobana
{
    public class WebRequestFailedException : Exception
    {
        public override string Message { get; }
        public string Type { get; }
        public string Param { get; }
        public string Code { get; }

        public WebRequestFailedException(ErrorInfo errorInfo)
        {
            this.Message = errorInfo.Error.Message;
            this.Type = errorInfo.Error.Type;
            this.Param = errorInfo.Error.Param;
            this.Code = errorInfo.Error.Code;
        }

    }
}
