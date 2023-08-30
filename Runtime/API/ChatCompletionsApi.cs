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

        private readonly Message systemMessage = new Message(Roles.SYSTEM, "");

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
        /// <exception cref="OpenAiRequestException"/>
        public async Task<ChatCompletionsResponse> CreateChatCompletionsRequest(ChatCompletionsRequest request)
        {
            string completionsApiUrl = apiUrl + "chat/completions";

            // Add past conversation data to request, make function null if not passed
            request = UpdateRequest(request);

            string requestJson = JsonConvert.SerializeObject(request, Formatting.Indented);
            Task<string> responseTask = MakeAPICall(completionsApiUrl, requestJson);

            // Add user responses to past conversations list
            UpdateUserResponses(request.Messages);

            string response = await responseTask;
            ChatCompletionsResponse chatResponse = JsonConvert.DeserializeObject<ChatCompletionsResponse>(response);

            // Add response to past conversations list
            UpdatePastResponses(chatResponse.Choices[0].Message);

            return chatResponse;
        }

        private ChatCompletionsRequest UpdateRequest(ChatCompletionsRequest request)
        {
            // Add previous conversation data to the request
            foreach (Message message in chatHistory)
            {
                request.Messages.Add(message);
            }

            // If a system message is available add
            if (systemMessage.Content != "")
            {
                request.Messages.Insert(0, systemMessage);
            }

            // If functions is not passed, make it null
            if (request.Functions.Count == 0)
            {
                request.Functions = null;
            }

            return request;
        }

        private void UpdateUserResponses(List<Message> responses)
        {
            // Add user responses to past conversations
            chatHistory.Clear();
            foreach (Message message in responses)
            {
                UpdatePastResponses(message);
            }
        }

        private void UpdatePastResponses(Message response)
        {
            // ignore if message is system role
            if (response.Role != Roles.SYSTEM)
            {
                this.chatHistory.Push(response);
            }

            // remove oldest conversation if exceed set range
            if (chatHistory.Count > ConversationHistoryMemory)
            {
                this.chatHistory.Pop();
            }

        }
    }

}
