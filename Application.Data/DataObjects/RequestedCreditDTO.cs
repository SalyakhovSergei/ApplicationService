using System;

namespace Application.Data.DataObjects
{
    public class RequestedCreditDTO
    {
        public int Id { get; set; }
        public int CreditType { get; set; }
        public double RequestedAmount { get; set; }
        public string RequestedCurrency { get; set; }
        public double AnnualSalary { get; set; }
        public double MonthSalary { get; set; }
        public string CompanyName { get; set; }
        public string Comment { get; set; }
    }
}