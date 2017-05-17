using System;

namespace WebApplication.Models
{
    public class TheListOfAppointmentsAndAccountingLoadsOfRadiographic
    {
        public int? Id { get; set; }
        public int? DentalPatientCardId { get; set; }
        public DentalPatientCard DentalPatientCard { get; set; }
        public string AssignedTypeOfResearch { get; set; }
        public int? MedicalDoctorId { get; set; }
        public MedicalDoctor MedicalDoctor { get; set; }
        public string TheOrganizationWhichConductedTheStudy { get; set; }
        public DateTime Date { get; set; }
        public float EquivalentDose { get; set; }
    }
}