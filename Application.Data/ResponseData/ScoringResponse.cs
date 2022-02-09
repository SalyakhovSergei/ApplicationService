using System;
using Newtonsoft.Json;

namespace Application.Data.ResponseData
{
    [Serializable]
    public class ScoringResponse
    {
        [JsonProperty("scoringStatus")]
        public bool? ScoringStatus { get; set; }
        
    }
}