using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityWebApp.Models
{
    public class Grade
    {
        public int StudentId { get; set; }
        public List<int> SubjectId { get; set; }
        public List<ResultGrade> Result { get; set; }

    }
}