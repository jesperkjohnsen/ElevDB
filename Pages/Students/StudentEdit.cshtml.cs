using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevDB.DataLogic;
using ElevDB.Models;
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

            if (studentId.HasValue)
            {
                 Student = StudentDatabase.GetStudentById(studentId.Value);
            }
            else
            {
                 Student = new Student();
            }

            if (Student == null)
            {
                RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(Student.StudentId > 0)
            {
                StudentDatabase.UpdateStudent(Student);
            }
            else
            {
                Nextcloud.AddUserToNextcloud(Student.NextcloudUsername, Student.FirstName, Student.LastName, Student.NextcloudOneTimePassword);
                StudentDatabase.CreateStudent(Student);
            }
            TempData["Message"] = "Student saved";
            return RedirectToPage("./StudentDetail", new { studentId = Student.StudentId });
        }
    }
}
