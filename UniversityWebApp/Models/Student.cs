using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityWebApp.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DoB { get; set; }
        public string GuardianName { get; set; }
        public string Nid { get; set; }
        public int UserId { get; set; }
        public Status Status { get; set; }

    }
}
