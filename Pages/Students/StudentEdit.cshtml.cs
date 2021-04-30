using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevDB.DataLogic;
using ElevDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElevDB.Pages.Students
{
    public class StudentEditModel : PageModel
    {
        [BindProperty]
        public Student Student { get; set; }

        public IActionResult OnGet(int? studentId)
        {
            if (HttpContext.Session.GetString("Administration") == "TRUE") // Der foretages login tjek
            {
                if (studentId.HasValue) // Hvis der kom studentId med i get requesten, bliver denne forsøgt hentet fra studentdb.
                {
                    Student = StudentDatabase.GetStudentById(studentId.Value);
                }
                else // Hvis der ikke var et student Id oprettes der er en nyt.
                {
                    Student = new Student();
                }

                if (Student == null) // Hvis student er null når koden når hertil, bliver man sendt til NotFound siden.
                {
                    RedirectToPage("/NotFound");
                }
                return Page();
            }
            else
            {
                return RedirectToPage("./StaffLogin");
            }
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(Student.StudentId > 0) // Hvis studentId er højere end 0 er der tale om en eksisterende bruger der skal opdateres.
            {
                StudentDatabase.UpdateStudent(Student);
            }
            else // Hvis studentId er 0 eller ikke sat må det være en ny bruger. Den oprettes i Nextcloud og StudentDB.
            {
                Nextcloud.AddUserToNextcloud(Student.NextcloudUsername, Student.FirstName, Student.LastName, Student.NextcloudOneTimePassword);
                StudentDatabase.CreateStudent(Student);
            }
            TempData["Message"] = "Student saved";
            return RedirectToPage("./StudentDetail", new { studentId = Student.StudentId });
        }
    }
}
