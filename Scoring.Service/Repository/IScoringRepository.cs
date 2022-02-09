using System.Threading.Tasks;
using Scoring.Service.Models;

namespace Scoring.Service.Repository
{
    public interface IScoringRepository
    {
        Response Evaluate();
        Task<Response> EvaluateAsync();
    }
}