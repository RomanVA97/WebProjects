using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CareerHistory
    {
        public int? Id { get; set; }
        public int? ResumeId { get; set; }
        public Resume Resume { get; set; }
        public string Organization { get; set; }
        public int? IndustryId { get; set; }
        public Industry Industry { get; set; }
        public DateTime Since { get; set; }
        public DateTime For { get; set; }
        public string ThePost { get; set; }
        public string JobResponsibilities { get; set; }
        public string Achievements { get; set; }
        public string Wages { get; set; }
        public string ReasonForLeaving { get; set; }
        

        public static List<CareerHistoryItem> GetItems(int? id)
        {
            ResumeContext RC = new ResumeContext();
            List<CareerHistory> list = RC.CareerHistory.Where(c => c.ResumeId == id).ToList();
            List<CareerHistoryItem> list2 = new List<CareerHistoryItem>();
            foreach (CareerHistory item in list)
            {
                CareerHistoryItem obj = new CareerHistoryItem();
                obj.Id = item.Id;
                obj.Organization = item.Organization;
                obj.Industry = RC.Industry.Find(item.IndustryId).Name;
                obj.Since = item.Since;
                obj.For = item.For;
                obj.ThePost = item.ThePost;
                obj.JobResponsibilities = item.JobResponsibilities;
                obj.Achievements = item.Achievements;
                obj.Wages = item.Wages;
                obj.ReasonForLeaving = item.ReasonForLeaving;
                list2.Add(obj);
            }
            return list2;
        }

    }
}