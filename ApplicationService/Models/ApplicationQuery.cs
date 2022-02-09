using System;
using System.ComponentModel;
using System.Security.Policy;
using Newtonsoft.Json;

namespace Application.Service.Models
{
    public class ApplicationQuery
    {
        
        public int Id { get; set; }
        [JsonProperty("ApplicationNum")]
        public string ApplicationNum { get; set; }

        [JsonProperty("ApplicationDate")]
        public DateTime ApplicationDate { get; set; }

        [JsonProperty("BranchBank")]
        public string BranchBank { get; set; }

        [JsonProperty("BranchBankAddress")]
        public string BranchBankAddress { get; set; }

        [JsonProperty("CreditManagerId")]
        public int CreditManagerId { get; set; }

        [JsonProperty("Applicant")]
        public Applicant Applicant { get; set; }

        [JsonProperty("RequestedCredit")]
        public RequestedCredit RequestedCredit { get; set; }
        

    }
}