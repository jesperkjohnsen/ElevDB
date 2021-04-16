using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ElevDB.DataLogic;


namespace ElevDB.Pages.Students
{
    public class StudentListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<Student> Students { get; set; }

        public void OnGet()
        {
           Students = StudentDatabase.GetStudents(SearchTerm);
        }
    }
}
