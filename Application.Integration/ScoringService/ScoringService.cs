using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using NLog;


namespace Application.Integration.ScoringService
{
    public class ScoringService: IScoringService
    {
        readonly Logger logger = LogManager.GetCurrentClassLogger();
        private const string evaluationQuery = "https://localhost:44307/Scoring/evaluate";

		private string GetJson(Task<WebResponse> response)
        {
            logger.Info("Отправлен запрос в сервис оценки");
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
                logger.Error(e.Message);
                throw;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw;
            }
        }
        
        public string Evaluate()
        {
            return GetJson(GetResponse(evaluationQuery));
        }
    }
}