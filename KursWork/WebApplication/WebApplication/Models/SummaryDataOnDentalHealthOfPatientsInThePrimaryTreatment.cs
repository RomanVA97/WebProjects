using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatment
    {
        public int? Id { get; set; }
        public int? MedicalDoctorId { get; set; }
        public MedicalDoctor MedicalDoctor { get; set; }


        ICollection<ElementSummaryDataOnDentalHealthOfPatientsInThePrimaryTreatment>
            ElementsSummaryDataOnDentalHealthOfPatientsInThePrimaryTreatment{ get; set; }
        public SummaryDataOnDentalHealthOfPatientsInThePrimaryTreatment()
        {
            ElementsSummaryDataOnDentalHealthOfPatientsInThePrimaryTreatment = 
                new List<ElementSummaryDataOnDentalHealthOfPatientsInThePrimaryTreatment>();
        }

    }
}