using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class MapOfDentalHealthStudent
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
        public int? HealthOrganizationId { get; set; }
        public HealthOrganization HealthOrganization { get; set; }
        public DateTime Data { get; set; }
        public int? InstitutionEducationId { get; set; }
        public InstitutionEducation InstitutionEducation { get; set; }
    }
}