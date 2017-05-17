using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DoctorsPrescriptionItem
    {
        public int? Id { get; set; }
        public string FIO { get; set; }
        public string Appointments { get; set; }
    }
}