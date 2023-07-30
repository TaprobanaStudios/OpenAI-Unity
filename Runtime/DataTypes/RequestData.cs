using System.Collections.Generic;
using Newtonsoft.Json;

namespace com.studios.taprobana
{
    [System.Serializable]
    public class ChatCompletionsRequest
    {
        [JsonProperty("model")]
        public string Model { get; set; } = "gpt-3.5-turbo";

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }

        [JsonProperty("temperature")]
        public int Temperature { get; set; } = 1;

        [JsonProperty("max_tokens")]
        public int MaxTokens { get; set; } = 256;

        [JsonProperty("top_p")]
        public int TopP { get; set; } = 1;

        [JsonProperty("frequency_penalty")]
        public int FrequencyPenalty { get; set; } = 0;

        [JsonProperty("presence_penalty")]
        public int PresencePenalty { get; set; } = 0;

        [JsonProperty("n")]
        public int  N { get; set; } = 1;

        public ChatCompletionsRequest()
        {
            this.Messages = new List<Message>();
        }
    }

}
