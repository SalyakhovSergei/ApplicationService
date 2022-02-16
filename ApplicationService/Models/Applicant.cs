using System;
using Newtonsoft.Json;

namespace Application.Service.Models
{
    public class Applicant
    {
        public int Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("dateBirth")]
        public DateTime DateBirth { get; set; }

        [JsonProperty("cityBirth")]
        public string CityBirth { get; set; }

        [JsonProperty("addressBirth")]
        public string AddressBirth { get; set; }

        [JsonProperty("addressCurrent")]
        public string AddressCurrent { get; set; }

        [JsonProperty("inn")]
        public int INN { get; set; }

        [JsonProperty("snils")]
        public string SNILS { get; set; }

        [JsonProperty("passportNum")]
        public string PassportNum { get; set; }

    }
}