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
        [BindProperty(SupportsGet = true)] // SearchTerm bruges til GetStudents, BindProperty sættes så HTML delen kan bruge dne.
        public string SearchTerm { get; set; }

        public IEnumerable<Student> Students { get; set; } // Der bliver et enumerable object array til at opbevare students, bliver til foreach i html koden.

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Administration") == "TRUE" || HttpContext.Session.GetString("Teacher") == "TRUE")
            {
                Students = StudentDatabase.GetStudents(SearchTerm); // Der foretages GetStudents på baggrund af searchTerm, som default alle studerende. 
                return Page();
            }
            else
            {
                return RedirectToPage("/Staff/StaffLogin");
            }            
        }
    }
}
