using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class PassportDentalHealthOfPupilsOfEducationalInstitutions
    {
        public int? Id { get; set; }
        public DateTime Since { get; set; }
        public DateTime For { get; set; }
        public int? MedicalDoctorId { get; set; }
        public MedicalDoctor MedicalDoctor { get; set; }
        public ICollection<ElementPassportDentalHealthOfPupilsOfEducationalInstitutions>
            ElementsPassportDentalHealthOfPupilsOfEducationalInstitutions{ get; set; }
        public PassportDentalHealthOfPupilsOfEducationalInstitutions()
        {
            ElementsPassportDentalHealthOfPupilsOfEducationalInstitutions =
                new List<ElementPassportDentalHealthOfPupilsOfEducationalInstitutions>();
        }
            
    }
}