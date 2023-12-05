using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace TestTools
{
    public class PostData
    {
        public static async Task<string> PostAsync(string apiLocation, Dictionary<string, string?> values)
        {
            HttpClient client = new HttpClient();

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(apiLocation, content);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        public static string Post(string apiLocation, Dictionary<string, object> values, int timeoutMS = 30000)
        {
            string result = "";
            try
            {
                Dictionary<string, string?> dString = values.ToDictionary(k => k.Key, k => k.Value.ToString());
                Task<string> response = PostAsync(apiLocation, dString);
                DateTime startTime = DateTime.Now;
                response.Wait(timeoutMS);
                result = response.Result;
            }
            catch (Exception e)
            {
                result = $"Error connecting to API: {e.Message}";
            }

            return result;
        }
    }
}