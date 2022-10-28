using System.Collections.Generic;

namespace DAL.Models
{
    public class SubjectResult
    {
        public int StudentId { get; set; }
        public List<int> SubjectId { get; set; }
        public List<Grade> Result { get; set; }
    }
}