using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            if (HttpContext.Session.GetString("Administration") == "TRUE")
            {
                if (subjectId.HasValue)
                {
                    Subject = StudentDatabase.GetSubjectById(subjectId.Value);
                }
                else
                {
                    Subject = new Subject();
                }
                if (Subject == null)
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

            if (Subject.SubjectId > 0)
            {
                StudentDatabase.UpdateSubject(Subject);
            }
            else
            {
                StudentDatabase.CreateSubject(Subject);
            }
            TempData["Message"] = "Subject saved";
            return RedirectToPage("./SubjectDetail", new { subjectId = Subject.SubjectId });
        }
    }
}
