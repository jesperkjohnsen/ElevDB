using System.Collections.Generic;
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
            if (HttpContext.Session.GetString("Administration") == "TRUE") // Login tjek
            {

                Grades = StudentDatabase.GetGrades(studentId); // Alle karakterer der er tilknyttet en given studerende hentes
                Student = StudentDatabase.GetStudentById(studentId); // Den gældende studerende hentes.
                Subjects = StudentDatabase.GetAllSubjects(); // Alle fag hentes
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
