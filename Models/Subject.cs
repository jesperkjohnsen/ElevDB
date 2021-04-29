using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElevDB.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        [Required,StringLength(255)]
        public string SubjectName { get; set; }
    }
}
