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


        public static List<ResumeItem> GetItemsForIndexPage()
        {
            List<ResumeItem> list = new List<ResumeItem>();
            ResumeContext RC = new ResumeContext();
            List<Resume> listResume = RC.Resume.Where(c => c.ImageByte != null).ToList();
            foreach (Resume item in listResume){
                ResumeItem obj = new ResumeItem();
                obj.Id = item.Id;
                obj.MiddleName = item.MiddleName;
                obj.Name = item.Name;
                obj.Surname = item.Surname;
                obj.DateOfBirth = item.DateOfBirth;
                obj.AccountId = item.AccountId;
                List<DesiredPositionItem> listDP = DesiredPosition.GetItems(item.Id);
                string[] arrayDesiredPosition = new string[listDP.Count];
                int i = 0;
                foreach(DesiredPositionItem itemDP in listDP)
                {
                    arrayDesiredPosition[i] = itemDP.ThePost;
                    i++;
                }
                obj.DesiredPosition = arrayDesiredPosition;
                obj.ImageByte = item.ImageByte;
                list.Add(obj);

            }
            RC.Dispose();


            return list;
        }

        public static List<ResumeItem> ToResumeItem(List<Resume> listResume)
        {
            List<ResumeItem> list = new List<ResumeItem>();
            ResumeContext RC = new ResumeContext();
            foreach (Resume item in listResume)
            {
                ResumeItem obj = new ResumeItem();
                obj.Id = item.Id;
                obj.MiddleName = item.MiddleName;
                obj.Name = item.Name;
                obj.Surname = item.Surname;
                obj.DateOfBirth = item.DateOfBirth;
                obj.AccountId = item.AccountId;
                List<DesiredPositionItem> listDP = DesiredPosition.GetItems(item.Id);
                string[] arrayDesiredPosition = new string[listDP.Count];
                int i = 0;
                foreach (DesiredPositionItem itemDP in listDP)
                {
                    arrayDesiredPosition[i] = itemDP.ThePost;
                    i++;
                }
                obj.DesiredPosition = arrayDesiredPosition;
                obj.ImageByte = item.ImageByte;
                list.Add(obj);

            }
            RC.Dispose();


            return list;
        }


    }
}