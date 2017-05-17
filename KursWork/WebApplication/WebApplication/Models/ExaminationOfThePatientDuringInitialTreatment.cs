using System;

namespace WebApplication.Models
{
    public class ExaminationOfThePatientDuringInitialTreatment
    {
        public int? Id { get; set; }
        public int? DentalPatientCardId { get; set; }
        public DentalPatientCard DentalPatientCard { get; set; }
        public int? MedicalDoctorId { get; set; }
        public MedicalDoctor MedicalDoctor { get; set; }
        public DateTime Date { get; set; }
        public string DiseaseOfTheCardiovascularSystem { get; set; }
        public string DiseaseOfTheNervousSystem { get; set; }
        public string DiseaseOfTheEndocrineSystem { get; set; }
        public string DiseaseOfTheDigestiveSystem { get; set; }
        public string DiseaseOfTheRespiratorySystem { get; set; }
        public string InfectiousDiseases { get; set; }
        public string AllergicReactions { get; set; }
        public string TheConstantUseOfDrugs { get; set; }
        public string HarmfulFactorsOfProductionEnvironment { get; set; }
        public string PregnancyPostpartumPeriod { get; set; }
        public string More { get; set; }
        public string ExternalExamination { get; set; }
        public string TheConditionOfTheSkin { get; set; }
        public string TheStatusOfTheRegionalLymphNodes { get; set; }
        public string TheConditionOfTheTemporomandibularJoint { get; set; }
    }
}