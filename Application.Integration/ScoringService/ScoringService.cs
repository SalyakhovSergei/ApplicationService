using System.Net;
using System.IO;


namespace Application.Integration.ScoringService
{
    public class ScoringService: IScoringService
    {
        private const string evaluationQuery = "https://localhost:5005/Scoring/getNonEvaluatedApps";

		private string GetJson(WebResponse response)
        {
            string answer = string.Empty;
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    answer = reader.ReadToEnd();
                }
            }
            response.Close();
            return answer;
        }

        private WebResponse GetResponse(string link)
        {
            WebRequest request = WebRequest.Create(link);
            request.Method = "Get";
            return request.GetResponse();
        }
        
        public string Evaluate()
        {
            return GetJson(GetResponse(evaluationQuery));
        }
    }
}