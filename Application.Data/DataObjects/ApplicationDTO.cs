using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Data.DataObjects
{
    public class ApplicationDTO
    {
        public int Id { get; set; }
        public string ApplicationNum { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string BranchBank { get; set; }
        public string BranchBankAddress { get; set; }
        public int CreditManagerId { get; set; }
        public ApplicantDTO Applicant { get; set; }
        public RequestedCreditDTO RequestedCredit { get; set; }
        public bool? ScoringStatus { get; set; }
        public DateTime ScoringDate { get; set; }
    }
}