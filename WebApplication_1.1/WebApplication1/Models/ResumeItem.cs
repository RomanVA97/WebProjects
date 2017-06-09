using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ResumeItem
    {
        public int? Id { get; set; }

        public int? AccountId { get; set; }

        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte[] ImageByte { get; set; }
        public string[] DesiredPosition { get; set; }
    }
}