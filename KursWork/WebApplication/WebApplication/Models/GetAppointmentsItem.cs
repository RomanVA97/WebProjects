using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class GetAppointmentsItem
    {
        public int? Id { get; set; }
        public string FIO { get; set; }
        public string PasportNumber { get; set; }
        public string Time { get; set; }
    }
}