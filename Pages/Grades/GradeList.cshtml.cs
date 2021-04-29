using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevDB.Models;
using ElevDB.DataLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElevDB.Pages.Grades
{
    public class GradeListModel : PageModel
    {

        public IEnumerable<Grade> Grades { get; set; }
        public Student Student { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
        public IEnumerable<SelectListItem> SubjectList { get; set; }

        public IActionResult OnGet(int studentId)
        {
            if (HttpContext.Session.GetString("Administration") == "TRUE")
            {

                Grades = StudentDatabase.GetGrades(studentId);
                Student = StudentDatabase.GetStudentById(studentId);
                Subjects = StudentDatabase.GetAllSubjects();
                var subjectList = new SelectList(Subjects, "SubjectId", "SubjectName");
                SubjectList = subjectList;

                return Page();
            }
            else
            {
                return RedirectToPage("./StaffLogin");
            }        
        }
    }
}
