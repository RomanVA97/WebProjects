using System;

namespace WebApplication.Models
{
    public class DiaryVisitsDPC
    {
        public int? Id { get; set; }
        public int? DentalPatientCardId { get; set; }
        public DentalPatientCard DentalPatientCard { get; set; }
        public DateTime Date { get; set; }
        public int? MedicalDoctorId { get; set; }
        public MedicalDoctor MedicalDoctor { get; set; }
        public string Entry { get; set; }
        public int? AListOfDoctorsAppointmentId { get; set; }
        public AListOfDoctorsAppointment AListOfDoctorsAppointment { get; set; }
    }
}