using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class TheOverallTreatmentPlanStudent
    {
        public int? Id { get; set; }
        public int? MapOfDentalHealthStudentId { get; set; }
        public MapOfDentalHealthStudent MapOfDentalHealthStudent { get; set; }
        public DateTime Date { get; set; }

        public string EmergencyCare { get; set; }
        public string PreventiveMeasures { get; set; }
        public string TherapeuticTreatment { get; set; }
        public string SurgicalTreatment { get; set; }
        public string OrthopedicTreatment { get; set; }
        public string More { get; set; }

    }
}