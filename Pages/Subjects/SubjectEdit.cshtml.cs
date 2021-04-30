using ElevDB.DataLogic;
using ElevDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElevDB.Pages.Subjects
{
    public class SubjectEditModel : PageModel
    {
        [BindProperty]
        public Subject Subject { get; set; }

        public IActionResult OnGet(int? subjectId)
        {
            if (HttpContext.Session.GetString("Administration") == "TRUE") // Der foretages login tjek
            {
                if (subjectId.HasValue) // Hvis der kommer et subjectId 
                {
                    Subject = StudentDatabase.GetSubjectById(subjectId.Value);
                }
                else // Hvis ikke der kom studentId med
                {
                    Subject = new Subject();
                }
                if (Subject == null) // Hvis der ikke er noget subject redirectes til notfound.
                {
                    RedirectToPage("/NotFound");
                }
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

            if (Subject.SubjectId > 0) // Hvis subjectId ikke er null og højere end 0, opdateres faget.
            {
                StudentDatabase.UpdateSubject(Subject);
            }
            else // Hvis subjectId ikke er sat oprettes et nyt fag.
            {
                StudentDatabase.CreateSubject(Subject);
            }
            TempData["Message"] = "Subject saved";
            return RedirectToPage("./SubjectDetail", new { subjectId = Subject.SubjectId });
        }
    }
}
