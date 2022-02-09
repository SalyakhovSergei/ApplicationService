using System;
using System.Threading.Tasks;
using Scoring.Service.Models;

namespace Scoring.Service.Repository
{
    public class ScoringRepository: IScoringRepository
    {
        public Response Evaluate()
        {
            Random random = new Random();
            int score = random.Next(1, 10);

            return new Response { scoringStatus = score < 5};
        }

        public async Task<Response> EvaluateAsync()
        {
           return await Task.Run(Evaluate);
        }
    }
}