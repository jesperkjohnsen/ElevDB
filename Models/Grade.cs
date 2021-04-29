using ElevDB.DataLogic;
using System;
using System.ComponentModel.DataAnnotations;

namespace ElevDB.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        [Required,StringLength(5)]
        public string GradeValue { get; set; }
        [Required]
        public DateTime GradeDate { get; set; }
    }
}
