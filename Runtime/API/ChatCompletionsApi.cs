using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace com.studios.taprobana
{
    public class ChatCompletionsApi : UnityOpenAI
    {

        private readonly Stack<Message> chatHistory = new Stack<Message>();

        /// <summary>
        /// The number of past conversations refereced.
        /// Setting this to a higher value will use more tokens
        /// </summary>
        public int ConversationHistoryMemory { get; set; } = 1;

        private readonly Message systemMessage = new Message()
        {
            Role = "system",
            Content = ""
        };

        public ChatCompletionsApi(string apiKey) : base(apiKey)
        {

        }

        /// <summary>
        /// Set the System Message
        /// </summary>
        /// <param name="message"></param>
        public void SetSystemMessage(string message)
        {
            systemMessage.Content = message;
        }

        /// <summary>
        /// Sends api request to chat completions api and returns response.
        /// Set ConversationHistoryMemory = 0 if N value is not equal to 1
        /// </summary>
        /// <param name="request"><see cref="ChatCompletionsRequest"/></param>
        /// <returns>See <see cref="ChatCompletionsResponse"/></returns>
        /// <exception cref="WebRequestFailedException"/>
        public async Task<ChatCompletionsResponse> CreateChatCompletionsRequest(ChatCompletionsRequest request)
        {
            string completionsApiUrl = apiUrl + "chat/completions";

            if (chatHistory.Count > 0)
            {
                foreach (Message message in chatHistory)
                {
                    request.Messages.Add(message);
                }
            }
            if (systemMessage.Content != "")
            {
                request.Messages.Insert(0, systemMessage);
            }
            string requestJson = JsonConvert.SerializeObject(request);
            Task<string> responseTask = MakeAPICall(completionsApiUrl, requestJson);

            UpdateUserResponses(request.Messages);

            string response = await responseTask;
            ChatCompletionsResponse chatResponse = JsonConvert.DeserializeObject<ChatCompletionsResponse>(response);
            UpdatePastResponses(chatResponse.Choices[0].Message);
            return chatResponse;
        }

        private void UpdateUserResponses(List<Message> responses)
        {
            foreach (Message message in responses)
            {
                UpdatePastResponses(message);
            }
        }

        private void UpdatePastResponses(Message response)
        {
            this.chatHistory.Push(response);

            if (chatHistory.Count > ConversationHistoryMemory)
            {
                this.chatHistory.Pop();
            }

        }
    }

}
