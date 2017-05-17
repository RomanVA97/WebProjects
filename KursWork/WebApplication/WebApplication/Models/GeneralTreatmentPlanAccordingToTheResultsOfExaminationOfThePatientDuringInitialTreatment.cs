using System;

namespace WebApplication.Models
{
    public class GeneralTreatmentPlanAccordingToTheResultsOfExaminationOfThePatientDuringInitialTreatment
    {
        public int? Id { get; set; }
        public int? DentalPatientCardId { get; set; }
        public DentalPatientCard DentalPatientCard { get; set; }
        public DateTime Date { get; set; }
        public int? MedicalDoctorId { get; set; }
        public MedicalDoctor MedicalDoctor { get; set; }

        public string EmergencyCare { get; set; }
        public string PreventiveMeasures { get; set; }
        public string HygieneEducation { get; set; }
        public string TherapeuticTreatment { get; set; }
        public string SurgicalTreatment { get; set; }
        public string OrthopedicTreatment { get; set; }
        public string OrthodonticTreatment { get; set; }
        public string AdditionalDiagnosticMeasures { get; set; }
        public string ConsultOtherSpecialists { get; set; }

    }
}