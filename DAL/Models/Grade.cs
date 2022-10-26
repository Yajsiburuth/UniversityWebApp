using System.Collections.Generic;

namespace DAL.Models
{
    public class Grade
    {
        public int StudentId { get; set; }
        public List<int> SubjectId { get; set; }
        public List<ResultGrade> Result { get; set; }

    }
}