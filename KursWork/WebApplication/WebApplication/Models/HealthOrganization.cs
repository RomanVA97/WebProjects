using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class HealthOrganization
    {
        public int? Id { get; set; }
        public string NameOfHealthOrganization { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
    }
}