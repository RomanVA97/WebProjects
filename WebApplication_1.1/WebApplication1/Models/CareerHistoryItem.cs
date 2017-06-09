using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CareerHistoryItem
    {
        public int? Id { get; set; }
        public string Organization { get; set; }
        public string Industry { get; set; }
        public DateTime Since { get; set; }
        public DateTime For { get; set; }
        public string ThePost { get; set; }
        public string JobResponsibilities { get; set; }
        public string Achievements { get; set; }
        public string Wages { get; set; }
        public string ReasonForLeaving { get; set; }

    }
}