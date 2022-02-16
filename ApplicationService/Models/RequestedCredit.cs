using System;
using Newtonsoft.Json;

namespace Application.Service.Models
{
    public class RequestedCredit
    {
        public int Id { get; set; }

        [JsonProperty("creditType")]
        public int CreditType { get; set; }

        [JsonProperty("requestedAmount")]
        public double RequestedAmount { get; set; }

        [JsonProperty("requestedCurrency")]
        public string RequestedCurrency { get; set; }

        [JsonProperty("annualSalary")]
        public double AnnualSalary { get; set; }

        [JsonProperty("monthSalary")]
        public double MonthSalary { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}