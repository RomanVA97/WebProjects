using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class LeafDailyAccountingWorkOfADentist
    {
        public int? Id { get; set; }
        public int? MedicalDoctorId { get; set; }
        public MedicalDoctor MedicalDoctor { get; set; }


        public ICollection<ElementLeafDailyAccountingWorkOfADentist> 
            ElementsLeafDailyAccountingWorkOfADentist { get; set; }
        public LeafDailyAccountingWorkOfADentist()
        {
            ElementsLeafDailyAccountingWorkOfADentist = new List<ElementLeafDailyAccountingWorkOfADentist>();
        }


    }
}