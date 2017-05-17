using System;

namespace WebApplication.Models
{
    public class RecordOnReceptionToTheDoctor
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int? MedicalDoctorId { get; set; }
        public MedicalDoctor MedicalDoctor { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public int? RoomPass { get; set; }
    }
}