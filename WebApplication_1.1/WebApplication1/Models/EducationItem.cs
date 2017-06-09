using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class EducationItem
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string Profile { get; set; }
        public DateTime Since { get; set; }
        public DateTime For { get; set; }
        public string Faculty { get; set; }
        public string Specialty { get; set; }
        public string DiplomaQualification { get; set; }
        public string FormOfTraining { get; set; }

    }
}