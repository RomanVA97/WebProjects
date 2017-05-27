using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Ability
    {
        public int? Id { get; set; }
        public int? ResumeId { get; set; }
        public Resume Resume { get; set; }
        public int? SkillId { get; set; }
        public Skill Skill { get; set; }
        public int? SkillLevelId { get; set; }
        public SkillLevel SkillLevel { get; set; }

        public static List<AbilityItem> GetItems(int? id)
        {
            List<AbilityItem> list = new List<AbilityItem>();
            ResumeContext RC = new ResumeContext();
            List<Ability> listA = RC.Ability.Where(c => c.ResumeId == id).ToList();
            foreach(Ability item in listA)
            {
                AbilityItem obj = new AbilityItem();
                try
                {
                    obj.Id = item.Id;
                    obj.Skill = RC.Skill.Find(item.SkillId).Name;
                    obj.SkillLevel = RC.SkillLevel.Find(item.SkillLevelId).Name;

                }
                catch
                {

                }

                list.Add(obj);
            }
            return list;
        }

    }
}