using System;

namespace WebApplication.Models
{
    public class MedicalDoctor
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public int? StructuralDivisionId { get; set; }
        public StructuralDivision StructuralDivision { get; set; }
        public int? MedicalDirectionId { get; set; }
        public MedicalDirection MedicalDirection { get; set; }
        public int? MedicalProfileId { get; set; }
        public MedicalProfile MedicalProfile { get; set; }
        public int? MedicalQualificationId { get; set; }
        public MedicalQualification MedicalQualification { get; set; }
        public int? DoctorQualificationId { get; set; }
        public DoctorQualification DoctorQualification { get; set; }
        public int Office { get; set; }
        public DateTime HoursOnEvenNumbers { get; set; }
        public DateTime HoursOnOddNumbers { get; set; }
    }
}