using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ElevDB.Models
{
    public class Student
    {   // Nedenstående værdier er tilsvarende indholdet af elevdatabasen, klassen bruges som model i koden andre steder. F.eks. student details.
        public int StudentId { get; set; }
        
        [Required, StringLength(255)]
        public string FirstName { get; set; }
        
        [Required, StringLength(255)]
        public string LastName { get; set; }
        
        [Required, StringLength(255)]
        public string Address { get; set; }

        [Required, StringLength(255)]
        public string City { get; set; }
        
        [Required, StringLength(255)]
        public string Zipcode { get; set; }

        [Required, StringLength(11)]
        public string CprNumber { get; set; }

        [Required, StringLength(255)]
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        [Required, StringLength(255)]
        public string NextcloudUsername { get; set; }
        
        [Required, MinLength(16),MaxLength(255)]
        public string NextcloudOneTimePassword { get; set; }

        public string FullName()
        {
            string fullName = FirstName + " " + LastName;
            return fullName;
        }
    }
}
