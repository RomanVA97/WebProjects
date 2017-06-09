using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DesiredPosition
    {
        public int? Id { get; set; }
        public int? ResumeId { get; set; }
        public Resume Resume { get; set; }
        public int? ThePostId { get; set; }
        public ThePost ThePost { get; set; }

        public static List<DesiredPositionItem> GetItems(int? id)
        {
            List<DesiredPositionItem> list = new List<DesiredPositionItem>();
            ResumeContext RC = new ResumeContext();
            List<DesiredPosition> listD = RC.DesiredPosition.Where(c => c.ResumeId == id).ToList();
            foreach(DesiredPosition item in listD)
            {
                DesiredPositionItem obj = new DesiredPositionItem();
                obj.PostId = item.Id;
                obj.ThePost = RC.ThePost.Find(item.ThePostId).Name;
                list.Add(obj);
            }

            return list;
        }

        public static List<DesiredPositionItem> All()
        {
            List<DesiredPositionItem> list = new List<DesiredPositionItem>();
            ResumeContext RC = new ResumeContext();
            List<DesiredPosition> listD = RC.DesiredPosition.ToList();
            foreach (DesiredPosition item in listD)
            {
                DesiredPositionItem obj = new DesiredPositionItem();
                obj.ThePost = RC.ThePost.Find(item.ThePostId).Name;
                obj.Id = item.ResumeId;
                obj.PostId = item.Id;
                list.Add(obj);
            }
            RC.Dispose();
            return list;
        }

    }
}