using ElevDB.DataLogic;
using ElevDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElevDB.Pages.Subjects
{
    public class SubjectDetailModel : PageModel
    {
        public Subject Subject { get; set; }

        public IActionResult OnGet(int SubjectId)
        {
            if (HttpContext.Session.GetString("Administration") == "TRUE") // Login tjek
            {
                Subject = StudentDatabase.GetSubjectById(SubjectId);
                if (Subject == null || SubjectId == 0) // Hvis subject er null er Id er 0 redirect til subjectlist
                {
                    return RedirectToPage("/Subjects/SubjectList");
                }
                return Page();
            }
            else
            {
                return RedirectToPage("./StaffLogin");
            }
        }
    }
}
