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

        /// <summary>
        /// Use Roles class to access available roles
        /// </summary>
        /// <param name="role"></param>
        /// <param name="content"></param>
        public Message(string role, string content)
        {
            this.Role = role;
            this.Content = content;
        }
    }

    public static class DataTypes
    {
        public static readonly string STRING = "string";
        public static readonly string ARRAY = "array";
        public static readonly string NUMBER = "number";
        public static readonly string OBJECT = "object";
    }

    public static class Roles
    {
        public static readonly string SYSTEM = "system";
        public static readonly string USER = "user";
        public static readonly string ASSISTANT = "assistant";
    }
}
