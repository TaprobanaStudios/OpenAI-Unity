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
        public List<Message> Messages { get; set; } = new List<Message>();

        [JsonProperty("functions", NullValueHandling = NullValueHandling.Ignore)]
        public List<Function> Functions { get; set; } = new List<Function>();

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
        public int N { get; set; } = 1;

        public ChatCompletionsRequest()
        {
            
        }

        public ChatCompletionsRequest(Message message)
        {
            this.Messages.Add(message);
        }

        public ChatCompletionsRequest(List<Message> messages)
        {
            this.Messages = messages;
        }

        public ChatCompletionsRequest(Message message, List<Function> functions)
        {
            this.Messages.Add(message);
            this.Functions = functions;
        }

        public ChatCompletionsRequest(Message message, Function function)
        {
            this.Messages.Add(message);
            this.Functions.Add(function);
        }

        public ChatCompletionsRequest(List<Message> messages, Function function)
        {
            this.Messages = messages;
            this.Functions.Add(function);
        }

        /// <summary>
        /// Add function to the request
        /// </summary>
        /// <param name="function"></param>
        public void AddFunction(Function function)
        {
            Functions.Add(function);
        }

        /// <summary>
        /// Add message to the request
        /// </summary>
        /// <param name="message"></param>
        public void AddMessage(Message message)
        {
            Messages.Add(message);
        }

    }

    #region Function Calling

    [System.Serializable]
    public class Function
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("parameters")]
        public Parameter Parameters { get; set; }

        /// <summary>
        /// Create function to perform a function call
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="parameters"></param>
        public Function(string name, string description, Parameter parameters)
        {
            this.Name = name;
            this.Description = description;
            this.Parameters = parameters;
        }
    }

    [System.Serializable]
    public class Parameter
    {
        [JsonProperty("type")]
        public string Type { get; set; } = DataTypes.OBJECT;

        [JsonProperty("properties")]
        public Dictionary<string, Property> Properties { get; set; } = new Dictionary<string, Property>();

        [JsonProperty("required")]
        public List<string> Required { get; set; } = new List<string>();

        /// <summary>
        /// Create parameter for the function call. 
        /// Use AddProperty() to add more properties. 
        /// Use DataTypes class to access data types available for propertyType
        /// </summary>
        public Parameter(string propertyName, string propertyType, string description)
        {
            Property property = new Property();
            property.Description = description;
            property.Type = propertyType;
            Properties.Add(propertyName, property);
        }

        public Parameter()
        {

        }

        /// <summary>
        /// Add a Property to the parameter. 
        /// Use DataTypes class to access data types available for propertyType
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="propertyType"></param>
        /// <param name="description"></param>
        public void AddProperty(string propertyName, string propertyType, string description)
        {
            Property property = new Property();
            property.Description = description;
            property.Type = propertyType;
            Properties.Add(propertyName, property);
        }
    }

    [System.Serializable]
    public class Property
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

    }

    #endregion

}
