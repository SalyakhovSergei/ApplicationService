using System;

namespace Application.Contracts.Models
{
    public class Application
    {
        public int Id { get; set; }
        public string ApplicationNum { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string BranchBank { get; set; }
        public string BranchBankAddress { get; set; }
        public int CreditManagerId { get; set; }
        public Applicant Applicant { get; set; }
        public RequestedCredit RequestedCredit { get; set; }
        public bool? ScoringStatus { get; set; } = null;
        public DateTime ScoringDate { get; set; }
    }
}