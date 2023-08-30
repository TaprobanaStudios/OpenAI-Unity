using System.Collections.Generic;
using Newtonsoft.Json;

namespace com.studios.taprobana
{
    public class Choice
    {
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("message")]
        public FunctionCallMessage Message { get; set; }

        [JsonProperty("finish_reason")]
        public string FinishReason { get; set; }
    }

    public class Usage
    {
        [JsonProperty("prompt_tokens")]
        public int PromptTokens { get; set; }

        [JsonProperty("completion_tokens")]
        public int CompletionTokens { get; set; }

        [JsonProperty("total_tokens")]
        public int TotalTokens { get; set; }
    }

    public class ChatCompletionsResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("choices")]
        public List<Choice> Choices { get; set; } = new List<Choice>();

        [JsonProperty("usage")]
        public Usage Usage { get; set; }

        /// <summary>
        /// Get the response for the context sent. 
        /// Pass index if multiple responses are expected
        /// </summary>
        /// <param name="index"></param>
        public string GetResponseMessage(int index = 0)
        {
            return Choices[index].Message.Content;
        }

        /// <summary>
        /// Get the function call response data. 
        /// Pass the index if multiple responses are expected
        /// </summary>
        /// <param name="index"></param>
        /// <returns>See <see cref="FunctionCall"/></returns>
        public FunctionCall GetFunctionCallResponse(int index = 0)
        {
            return Choices[index].Message.FunctionCall;
        }
    }

    #region Function Call Response

    public class FunctionCallMessage : Message
    {
        public FunctionCallMessage(string role, string content, FunctionCall functionCall): base(role, content)
        {
            this.FunctionCall = functionCall;
        }

        [JsonProperty("function_call")]
        public FunctionCall FunctionCall { get; set; }
    }

    public class FunctionCall
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("arguments")]
        public string Arguments { get; set; }

    }

    #endregion

    #region Error Responses
    public class ErrorDetails
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("param")]
        public string Param { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class ErrorInfo
    {
        [JsonProperty("error")]
        public ErrorDetails Error { get; set; }
    }
    # endregion
}