using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System;

namespace com.studios.taprobana
{
    public class UnityOpenAI
    {

        protected readonly string apiUrl = "https://api.openai.com/v1/";

        private readonly HttpClient httpClient;

        public UnityOpenAI(string apiKey)
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
        }

        /// <summary>
        /// Update API Key for OpenAI
        /// </summary>
        /// <param name="apiKey"></param>
        public void SetApiKey(string apiKey)
        {
            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
        }

        protected async Task<string> MakeAPICall(string apiUrl, string jsonData)
        {
            try
            {
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                using var response = await httpClient.PostAsync(apiUrl, content);
                string responseMessage = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    ErrorInfo error = JsonConvert.DeserializeObject<ErrorInfo>(responseMessage);
                    throw new WebRequestFailedException(error);
                }

                return responseMessage;
            }
            catch (WebRequestFailedException)
            {
                throw;
            }
            catch (Exception exception)
            {
                ErrorInfo errorInfo = new ErrorInfo();
                errorInfo.Error.Message = exception.Message;
                throw new WebRequestFailedException(errorInfo);
            }
        }
    }
}
