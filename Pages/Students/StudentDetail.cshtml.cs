using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ElevDB.Models;
using ElevDB.DataLogic;
using Microsoft.AspNetCore.Http;

namespace ElevDB.Pages.Students
{
    public class StudentDetailModel : PageModel
    {
        public Student Student { get; set; }

        public IActionResult OnGet(int studentId)
        {
            
            if (HttpContext.Session.GetString("Administration") == "TRUE" || HttpContext.Session.GetString("Teacher") == "TRUE")
            {
                Student = StudentDatabase.GetStudentById(studentId);
                if (Student == null || studentId == 0)
                {
                    return RedirectToPage("/Students/StudentList");
                }
                return Page();
            }
            else
            {
                return RedirectToPage("/Staff/StaffLogin");
            }

        }

        public void OnPostDelete(Student student)
        {
            StudentDatabase.DeleteStudent(student);
            RedirectToPage("/Students/StudentList");
        }

    }
}