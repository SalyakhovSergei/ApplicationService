using System;

namespace Application.Contracts.Models
{
    public class RequestedCredit
    {
        public int Id { get; set; }
        public int CreditType { get; set; }
        public double RequestedAmount { get; set; }
        public string RequestedCurrency { get; set; }
    }
}