using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevDB.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public string GradeValue { get; set; }
        public DateTime GradeDate { get; set; }
    }
}
