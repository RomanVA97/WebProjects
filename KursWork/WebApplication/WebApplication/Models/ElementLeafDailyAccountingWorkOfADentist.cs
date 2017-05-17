using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ElementLeafDailyAccountingWorkOfADentist
    {
        public int? LeafDailyAccountingWorkOfADentistId { get; set; }
        public LeafDailyAccountingWorkOfADentist LeafDailyAccountingWorkOfADentist { get; set; }
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public int TheNumberOfFullYears { get; set; }
        public string Adress { get; set; }
        public string KodeMKB_10 { get; set; }
        public string Description { get; set; }

        public string Treatment { get; set; }

        public string ViewVisit { get; set; }

        public string CodeKeyGroup { get; set; }

        public string IntactDentition { get; set; }

        public int K { get; set; }

        public int P { get; set; }

        public int U { get; set; }

        public int Just { get; set; }

        public string OHI_S_PL1 { get; set; }

        public string KPI { get; set; }
    }
}