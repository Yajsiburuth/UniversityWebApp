using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityWebApp.Models
{
    public class Grade
    {
        public int StudentId;
        public int SubjectId { get; set; }
        public Student Subject { get; set; }
        public ResultGrade Result { get; set; }

    }
}