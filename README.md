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

### Chat Completions Demo
```csharp
public class ChatCompletionsDemo : MonoBehaviour
{
    private ChatCompletionsApi chatCompletionsApi;
    private readonly string apiKey = "YOUR_API_KEY";

    private void Start()
    {
        chatCompletionsApi = new(apiKey);
        chatCompletionsApi.ConversationHistoryMemory = 5;
        chatCompletionsApi.SetSystemMessage("You are Mario");
    }

    public async void GetResponse(string userInput)
    {
        try
        {
            ChatCompletionsRequest chatCompletionsRequest = new ChatCompletionsRequest();
            Message message = new(Roles.USER, userInput);

            chatCompletionsRequest.AddMessage(message);

            ChatCompletionsResponse res = await chatCompletionsApi.CreateChatCompletionsRequest(chatCompletionsRequest);
            string response = res.GetResponseMessage();
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
### Function Call Demo
Below is an example of using OpenAI function calls.
Refer: https://platform.openai.com/docs/guides/gpt/function-calling

```csharp
public class FunctionCallingDemo : MonoBehaviour
{
    private ChatCompletionsApi chatCompletionsApi;
    private readonly string apiKey = "YOUR_API_KEY";

    private void Start()
    {
        chatCompletionsApi = new ChatCompletionsApi(apiKey);
        chatCompletionsApi.ConversationHistoryMemory = 0;
        chatCompletionsApi.SetSystemMessage("You are a Video game expert");
    }

    private Function GetFunctionData()
    {
        string description = "Generate 1 questions of the provided CHARACTER, generate 2 answers (answer_1 and answer_2), " +
            "one answer should be correct and other should be incorrect, the correct answer out of them should be given in parameter correct";
        string name = "trivia_data";

        Parameter parameters = new Parameter();
        parameters.AddProperty("question", DataTypes.STRING, "question");
        parameters.AddProperty("answer_1", DataTypes.STRING, "answer 1");
        parameters.AddProperty("answer_2", DataTypes.STRING, "answer 2");
        parameters.AddProperty("correct", DataTypes.STRING, "correct answer out of answer 1 and answer 2");

        Function function = new Function(name, description, parameters);
        return function;
    }

    async public void GetResponse(string input)
    {
        try
        {
            ChatCompletionsRequest request = new ChatCompletionsRequest();
            Message message = new(Roles.USER, input);
            request.AddMessage(message);
            request.AddFunction(GetFunctionData());

            ChatCompletionsResponse res = await chatCompletionsApi.CreateChatCompletionsRequest(request);

            Debug.Log(res.GetFunctionCallResponse().Arguments);

            // input:- CHARACTER: Mario
            // Response
            //{
            //    "question": "In which year was the first Mario game released?",
            //    "answer_1": "1981",
            //    "answer_2": "1985",
            //    "correct": "1985"
            //}
        }
        catch (OpenAiRequestException exception)
        {
            Debug.LogError(exception);
        }
    }
}
```
