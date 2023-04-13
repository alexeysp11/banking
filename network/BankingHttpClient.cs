using System.Collections.Generic; 
using System.Net.Http;
using System.Threading.Tasks;

namespace Banking.Network
{
    public class BankingHttpClient 
    {
        private static readonly HttpClient client = new HttpClient();
        
        public static string Get(string requestUri)
        {
            string responseString = string.Empty; 
            try
            {
                Task.Run(async () => {
                    responseString = await client.GetStringAsync(requestUri);
                    System.Console.WriteLine(responseString);
                }).Wait();
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            return responseString; 
        }

        public static string Post(string requestUri, Dictionary<string, string> values) 
        {
            string responseString = string.Empty; 
            try
            {
                Task.Run(async () => {
                    var response = await client.PostAsync(requestUri, new FormUrlEncodedContent(values));
                    responseString = await response.Content.ReadAsStringAsync();
                }).Wait();
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            return responseString; 
        }
    }
}
