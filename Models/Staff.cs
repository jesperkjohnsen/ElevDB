using System.ComponentModel.DataAnnotations;

namespace ElevDB.Models
{
    public class Staff
    {
        public int staffId;

        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        public bool Teacher { get; set; }
        public bool Administration { get; set; }
        [MinLength(2),StringLength(255)]
        public string Username { get; set; }
        [MinLength(8),StringLength(255)]
        public string Password { get; set; }

        public string FullName()
        {
            string fullName = FirstName + " " + LastName;
            return fullName;
        }
    }
}
