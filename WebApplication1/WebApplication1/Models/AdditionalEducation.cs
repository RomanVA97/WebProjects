using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class AdditionalEducation
    {
        public int? Id { get; set; }
        public int? ResumeId { get; set; }
        public Resume Resume { get; set; }
        public string Name { get; set; }
        public int? TheTypeOfTrainingId { get; set; }
        public TheTypeOfTraining TheTypeOfTraining { get; set; }
        public string YearOfCommencementOfStudy { get; set; }
        public string TheDurationOfTraining { get; set; }
        public string NameOfSchool { get; set; }
        public string Teacher { get; set; }


        public static List<AdditionalEducationItem> GetItems(int? id)
        {
            List<AdditionalEducationItem> list = new List<AdditionalEducationItem>();
            ResumeContext RC = new ResumeContext();
            List<AdditionalEducation> ListA = RC.AdditionalEducation.Where(c => c.ResumeId == id).ToList();
            foreach(AdditionalEducation item in ListA)
            {

                AdditionalEducationItem obj = new AdditionalEducationItem();
                obj.Id = item.Id;
                obj.Name = item.Name;
                obj.TheTypeOfTraining = RC.TheTypeOfTraining.Find(item.TheTypeOfTrainingId).Name;
                obj.YearOfCommencementOfStudy = item.YearOfCommencementOfStudy;
                obj.TheDurationOfTraining = item.TheDurationOfTraining;
                obj.NameOfSchool = item.NameOfSchool;
                obj.Teacher = item.Teacher;
                list.Add(obj);
            }


            return list;
        }

    }
}