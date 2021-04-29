using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevDB.DataLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElevDB.Pages.Staff
{
    public class StaffListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<Models.Staff> AllStaff { get; set; }

        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetString("Administration") == "TRUE")
            {
                AllStaff = StudentDatabase.GetStaff(SearchTerm);
                return Page();
            }
            else
            {
                return RedirectToPage("./StaffLogin");
            }            
        }
    }
}
