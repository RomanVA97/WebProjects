using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class MyNotesToTheDoctorsItem
    {
        public int? Id { get; set; }
        public string FIO { get; set; }
        public DateTime Date { get; set; }
        public string Office { get; set; }
    }
}