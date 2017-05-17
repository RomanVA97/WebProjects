namespace WebApplication.Models
{
    public class AListOfDoctorsAppointment
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int? MedicalDoctorId { get; set; }
        public MedicalDoctor MedicalDoctor { get; set; }
        public string Appointments { get; set; }
    }
}