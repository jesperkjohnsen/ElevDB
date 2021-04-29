using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevDB.Models;
using ElevDB.DataLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace ElevDB.Pages.Grades
{
    public class GradeAddModel : PageModel
    {
        [BindProperty]
        public Grade Grade { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        public IEnumerable<SelectListItem> SubjectList { get; set; }

        public IActionResult OnGet(int studentId)
        {

            if (HttpContext.Session.GetString("Administration") == "TRUE")
            {
                Subjects = StudentDatabase.GetAllSubjects();
                var subjectList = new SelectList(Subjects, "SubjectId", "SubjectName");
                SubjectList = subjectList;
                Grade = new Grade
                {
                    StudentId = studentId
                };
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

            else
            {
                StudentDatabase.AddGrade(Grade);
            }
            TempData["Message"] = "Grade saved";
            return RedirectToPage("./GradeList", new { studentId = Grade.GradeId});
        }
    }
}
