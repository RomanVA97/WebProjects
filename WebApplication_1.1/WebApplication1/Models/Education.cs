using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Education
    {
        public int? Id { get; set; }
        public int? ResumeId { get; set; }
        public Resume Resume { get; set; }
        public string Name { get; set; }
        public int? LevelId { get; set; }
        public Level Level { get; set; }
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }
        public DateTime Since { get; set; }
        public DateTime For { get; set; }
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public string DiplomaQualification { get; set; }
        public int? FormOfTrainingId { get; set; }
        public FormOfTraining FormOfTraining { get; set; }



        public static List<EducationItem> GetItems(int? id)
        {
            List<EducationItem> list = new List<EducationItem>();
            ResumeContext RC = new ResumeContext();
            List<Education> listE = RC.Education.Where(c => c.ResumeId == id).ToList();
            foreach(Education item in listE)
            {
                EducationItem obj = new EducationItem();
                obj.Id = item.Id;
                obj.Name = item.Name;
                obj.Level = RC.Level.Find(item.LevelId).Name;
                obj.Profile = RC.Profile.Find(item.ProfileId).Name;
                obj.Since = item.Since;
                obj.For = item.For;
                obj.Faculty = item.Faculty;
                obj.Specialty = item.Specialty;
                obj.DiplomaQualification = item.DiplomaQualification;
                obj.FormOfTraining = RC.FormOfTraining.Find(item.FormOfTrainingId).Name;
                list.Add(obj);
            }

            RC.Dispose();
            return list;
        }









    }
}