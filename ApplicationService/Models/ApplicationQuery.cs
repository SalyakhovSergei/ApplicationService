using System;
using System.ComponentModel;
using System.Security.Policy;
using Newtonsoft.Json;

namespace Application.Service.Models
{
    public class ApplicationQuery
    {
        
        public int Id { get; set; }
        [JsonProperty("applicationNum")]
        public string ApplicationNum { get; set; }

        [JsonProperty("applicationDate")]
        public DateTime ApplicationDate { get; set; }

        [JsonProperty("branchBank")]
        public string BranchBank { get; set; }

        [JsonProperty("branchBankAddress")]
        public string BranchBankAddress { get; set; }

        [JsonProperty("creditManagerId")]
        public int CreditManagerId { get; set; }

        [JsonProperty("applicant")]
        public Applicant Applicant { get; set; }

        [JsonProperty("requestedCredit")]
        public RequestedCredit RequestedCredit { get; set; }
        

    }
}