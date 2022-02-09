using System;
using Newtonsoft.Json;

namespace Application.Service.Models
{
    public class RequestedCredit
    {
        public int Id { get; set; }

        [JsonProperty("CreditType")]
        public int CreditType { get; set; }

        [JsonProperty("RequestedAmount")]
        public double RequestedAmount { get; set; }

        [JsonProperty("RequestedCurrency")]
        public string RequestedCurrency { get; set; }

        [JsonProperty("AnnualSalary")]
        public double AnnualSalary { get; set; }

        [JsonProperty("MonthSalary")]
        public double MonthSalary { get; set; }

        [JsonProperty("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("Comment")]
        public string Comment { get; set; }
    }
}