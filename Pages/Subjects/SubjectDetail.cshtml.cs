using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevDB.DataLogic;
using ElevDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElevDB.Pages.Subjects
{
    public class SubjectDetailModel : PageModel
    {
        public Subject Subject { get; set; }

        public IActionResult OnGet(int SubjectId)
        {
            Subject = StudentDatabase.GetSubjectById(SubjectId);
            if (Subject == null || SubjectId == 0)
            {
                return RedirectToPage("/Subjects/SubjectList");
            }
            return Page();
        }
    }
}
