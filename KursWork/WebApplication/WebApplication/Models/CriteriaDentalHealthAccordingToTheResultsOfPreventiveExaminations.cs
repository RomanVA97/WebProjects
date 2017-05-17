using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CriteriaDentalHealthAccordingToTheResultsOfPreventiveExaminations
    {
        public int? Id { get; set; }
        public int? MapOfDentalHealthStudentId { get; set; }
        public MapOfDentalHealthStudent MapOfDentalHealthStudent { get; set; }
        public DateTime Date { get; set; }
        public int? MedicalDoctorId { get; set; }
        public MedicalDoctor MedicalDoctor { get; set; }


        public int SchoolClass { get; set; }
        public int OHI_S1 { get; set; }
        public int OHI_S2 { get; set; }
        public int OHI_S3 { get; set; }
        public int RatingIndexOHI_S { get; set; }
        public int KPI1 { get; set; }
        public int KPI2 { get; set; }
        public int KPI3 { get; set; }
        public int RatingIndexKPI { get; set; }
        public int K { get; set; }
        public int P { get; set; }
        public int U { get; set; }
        public int KP { get; set; }
        public int RatingIndexKPU { get; set; }
        public int UIK { get; set; }
        public int RatingIndexUIK { get; set; }

    }
}