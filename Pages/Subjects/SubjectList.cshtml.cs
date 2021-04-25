using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ElevDB.DataLogic;
using ElevDB.Models;

namespace ElevDB.Pages.Subjects
{
    public class SubjectListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<Subject> Subjects { get; set; }

        public void OnGet()
        {
            StudentDatabase.GetSubjects(SearchTerm);
        }
    }
}
