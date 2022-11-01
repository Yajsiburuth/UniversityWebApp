using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class SubjectResult
    {
        public List<int> SubjectResultId { get; set; }
        public int StudentId { get; set; }
        public List<Int16> SubjectId { get; set; }
        public List<Grade> Result { get; set; }
    }
}