using Newtonsoft.Json;


namespace com.studios.taprobana
{
    [System.Serializable]
    public class Message
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
