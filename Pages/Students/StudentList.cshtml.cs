using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ElevDB.DataLogic;
using Microsoft.AspNetCore.Http;

namespace ElevDB.Pages.Students
{
    public class StudentListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<Student> Students { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Administration") == "TRUE" || HttpContext.Session.GetString("Teacher") == "TRUE")
            {
                Students = StudentDatabase.GetStudents(SearchTerm);
                return Page();
            }
            else
            {
                return RedirectToPage("/Staff/StaffLogin");
            }
            
        }
    }
}
