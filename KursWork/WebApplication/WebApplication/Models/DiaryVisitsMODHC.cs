using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DiaryVisitsMODHC
    {
        public int? Id { get; set; }
        public int? MapOfDentalHealthStudentId { get; set; }
        public MapOfDentalHealthStudent MapOfDentalHealthStudent { get; set; }
        public DateTime Date { get; set; }
        public int? MedicalDoctorId { get; set; }
        public MedicalDoctor MedicalDoctor { get; set; }


        public string Entry { get; set; }
        public int? AListOfDoctorsAppointmentId { get; set; }
        public AListOfDoctorsAppointment AListOfDoctorsAppointment { get; set; }
    }
}