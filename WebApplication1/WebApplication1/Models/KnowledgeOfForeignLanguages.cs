using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class KnowledgeOfForeignLanguages
    {
        public int? Id { get; set; }
        public int? ResumeId { get; set; }
        public Resume Resume { get; set; }
        public int? LanguageId { get; set; }
        public Language Language { get; set; }
        public int? TheLevelOfLanguageLearningId { get; set; }
        public TheLevelOfLanguageLearning TheLevelOfLanguageLearning { get; set; }


        public static List<KnowledgeOfForeignLanguagesItem> GetItems(int? id)
        {
            List<KnowledgeOfForeignLanguagesItem> list = new List<KnowledgeOfForeignLanguagesItem>();
            ResumeContext RC = new ResumeContext();
            List<KnowledgeOfForeignLanguages> ListK = RC.KnowledgeOfForeignLanguages.Where(c => c.ResumeId == id).ToList();
            foreach(KnowledgeOfForeignLanguages item in ListK)
            {
                KnowledgeOfForeignLanguagesItem obj = new KnowledgeOfForeignLanguagesItem();
                obj.Id = item.Id;
                obj.Language = RC.Language.Find(item.LanguageId).Name;
                obj.TheLevelOfLanguageLearning = RC.TheLevelOfLanguageLearning.Find(item.TheLevelOfLanguageLearningId).Name;
                list.Add(obj);
            }

            return list;
        }

    }
}