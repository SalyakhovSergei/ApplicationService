using System;

namespace Application.Data.ResponseData
{
    public class ApplicationResponse
    {
        public string ApplicationNum { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string BranchBank { get; set; }
        public string BranchBankAddress { get; set; }
        public int CreditManagerId { get; set; }
        public ApplicantResponse Applicant { get; set; }
        public RequestedCreditResponse RequestedCredit { get; set; }
        public bool? ScoringStatus { get; set; } = null;
        public DateTime ScoringDate { get; set; }
    }
}
