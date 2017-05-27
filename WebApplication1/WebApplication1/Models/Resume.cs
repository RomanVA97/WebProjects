using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Resume
    {
        public int? Id { get; set; }

        public int? AccountId { get; set; }

        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Adress { get; set; }
        public string HomeNumber { get; set; }
        public string WorkNumber { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string MaritalStatus { get; set; }
        public string TheCompositionOfTheFamily { get; set; }
        public string DriversLicense { get; set; }
        public string CarBrand { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageByte { get; set; }


        public string TheBusinessAndPsychologicalQualities { get; set; }
        public string ProfessionalSkills { get; set; }
        public string Hobbies { get; set; }

        //
        public string WorkingConditions { get; set; }
        public string ProfessionalTasks { get; set; }
        public string ForMoreInformation { get; set; }

        public string Salary { get; set; }
        public string MinSalary { get; set; }
        public string NormSalary { get; set; }

        public bool SalaryChek { get; set; }
        public bool ThePercentage { get; set; }
        public bool SalaryBonus { get; set; }
        public bool SalaryPercentage { get; set; }
        public string Comment { get; set; }


        public string Income { get; set; }
        public string TheProspectOfJobGrowth { get; set; }
        public string ToGetTheNecessaryExperience { get; set; }
        public string ToImproveTheProfessionalLevel { get; set; }
        public string ToDemonstrateTheirAbilities { get; set; }
        public string AHighLevelOfAutonomy { get; set; }
        public string TheStabilityOfTheCompany { get; set; }
        public string ActivitiesOfTheCompany { get; set; }
        public string WorkingConditionsInTheWorkplace { get; set; }
        public string RelationsWithTheLeadership { get; set; }
        public string SomethingElse { get; set; }
        public int? SourceOfInformationId { get; set; }
        public SourceOfInformation SourceOfInformation { get; set; }
    }
}