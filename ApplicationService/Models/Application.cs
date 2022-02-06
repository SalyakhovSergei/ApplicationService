using System;
using System.ComponentModel;
using System.Security.Policy;

namespace Application.Service.Models
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
        [DefaultValue(false)] 
        public bool ScoringStatus { get; set; }
        [DefaultValue(null)] 
        public DateTime ScoringDate { get; set; }

    }
}