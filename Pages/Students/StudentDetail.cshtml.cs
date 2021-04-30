using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ElevDB.Models;
using ElevDB.DataLogic;
using Microsoft.AspNetCore.Http;

namespace ElevDB.Pages.Students
{
    public class StudentDetailModel : PageModel
    {
        public Student Student { get; set; } // Bruges til at gemme data samt opbygge detail siden.

        public IActionResult OnGet(int studentId)
        {   // Der tjekkes hvorvidt der er blevet logget ind og brugeren enten er administrator eller underviser. Hvis ikke foretages der redirect til login siden.
            // Der bruges typen IActionResult så der kan bruges logik ift. til hvilken side der returneres.
            if (HttpContext.Session.GetString("Administration") == "TRUE" || HttpContext.Session.GetString("Teacher") == "TRUE")
            {
                Student = StudentDatabase.GetStudentById(studentId); // Den studerende hentes på baggrund af Id.
                if (Student == null || studentId == 0)
                { 
                    return RedirectToPage("/Students/StudentList"); // Hvis der ikke findes en studerende med det Id, sendes man til studenlist.
                }
                return Page();
            }
            else
            {
                return RedirectToPage("/Staff/StaffLogin");
            }
        }
        public void OnPostDelete(Student student) // Hvis der trykkes på slet knappen, foretages delete og efterfølgende redirect til Studentlist.
        {
            StudentDatabase.DeleteStudent(student);
            RedirectToPage("/Students/StudentList");
        }
    }
}