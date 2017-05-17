using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class City
    {
        public int? Id { get; set; }
        public int? RegionId { get; set; }
        public Region Region { get; set; }
        public string Name { get; set; }
    }
}