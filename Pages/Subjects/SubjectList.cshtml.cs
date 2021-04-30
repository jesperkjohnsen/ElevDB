using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ElevDB.DataLogic;
using ElevDB.Models;
using Microsoft.AspNetCore.Http;

namespace ElevDB.Pages.Subjects
{
    public class SubjectListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<Subject> Subjects { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Administration") == "TRUE") // Login tjek
            {
                Subjects = StudentDatabase.GetSubjects(SearchTerm); // 
                return Page();
            }
            else
            {
                return RedirectToPage("/Staff/StaffLogin");
            }            
        }
    }
}
 