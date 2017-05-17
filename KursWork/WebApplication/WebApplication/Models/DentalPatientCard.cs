using System;

namespace WebApplication.Models
{
    public class DentalPatientCard
    {
        public int? Id { get; set; }
        public int? HealthOrganizationId { get; set; }
        public HealthOrganization HealthOrganization { get; set; }
        public DateTime Data { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}