using Newtonsoft.Json;

namespace Scoring.Service.Models
{
    public class Response
    {
        [JsonProperty("scoringStatus")]
        public bool ScoringStatus { get; set; }
    }
}