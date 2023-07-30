# OpenAI-Unity
OpenAI package for Unity. This package could be directly used in Unity to connect with OpenAI api.

## Importing Package
To import the package to the project follow the below steps,
1. Open Package Manager in Unity (`Window -> Package Manager`)
2. Select `+` and select `Add package from git URL`
3. Paste https://github.com/TaprobanaStudios/OpenAI-Unity.git
4. Click `Add`

## Using Completions API
Refer https://platform.openai.com/docs/api-reference/chat/create for more information about Completions API
### ChatCompletions Request
```csharp
Message message = new Message()
{
    Role = "user",
    Content = userInput
};

ChatCompletionsRequest chatCompletionsRequest = new ChatCompletionsRequest();
chatCompletionsRequest.Model = "gpt-3.5-turbo";
chatCompletionsRequest.PresencePenalty = 0;
chatCompletionsRequest.FrequencyPenalty = 0;
chatCompletionsRequest.MaxTokens = 256;
chatCompletionsRequest.Temperature = 1;
chatCompletionsRequest.TopP = 1;
chatCompletionsRequest.N = 1;
chatCompletionsRequest.Messages.Add(message);
```
### Example Monobehaviour
```csharp
public class ChatCompletionsDemo : MonoBehaviour
{
    private ChatCompletionsApi chatCompletionsApi;
    private readonly string apiKey = "YOUR API KEY";

    private void Start()
    {
        chatCompletionsApi = new(apiKey);
    }

    public async void GetResponse(string userInput)
    {
        try
        {
            chatCompletionsApi.ConversationHistoryMemory = 5;
            chatCompletionsApi.SetSystemMessage("You are Mario");

            ChatCompletionsRequest chatCompletionsRequest = new ChatCompletionsRequest();
            Message message = new()
            {
                Role = "user",
                Content = userInput
            };
            chatCompletionsRequest.Messages.Add(message);

            ChatCompletionsResponse res = await chatCompletionsApi.CreateChatCompletionsRequest(chatCompletionsRequest);
            string response = res.Choices[0].Message.Content;
            Debug.Log(response);
        }
        catch (OpenAiRequestException exception)
        {
            // exception.Code
            // exception.Param
            // exception.Type
            // exception.Message
            Debug.LogError(exception);
        }
    }
}
```

