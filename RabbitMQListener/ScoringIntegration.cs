using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace RabbitMQListener
{
    public class ScoringIntegration
    {
        private const string evaluationQuery = "https://localhost:5005/Scoring/evaluate";

        private string GetJson(Task<WebResponse> response)
        {
            string answer = string.Empty;
            using (Stream stream = response.Result.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    answer = reader.ReadToEnd();
                }
            }
            response.Result.Close();
            return answer;
        }

        private async Task<WebResponse> GetResponse(string link)
        {
            try
            {
                WebRequest request = WebRequest.Create(link);
                request.Method = "Post";
                return await request.GetResponseAsync();
            }
            catch (WebException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string Evaluate()
        {
            return GetJson(GetResponse(evaluationQuery));
        }
    }
}