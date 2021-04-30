using System.Collections.Generic;
using ElevDB.DataLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElevDB.Pages.Staff
{
    public class StaffListModel : PageModel
    {
        [BindProperty(SupportsGet = true)] // Sættes så HTML søgefunktionen kan sætte værdien.
        public string SearchTerm { get; set; }

        public IEnumerable<Models.Staff> AllStaff { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Administration") == "TRUE") // Der foretages login tjek
            {
                AllStaff = StudentDatabase.GetStaff(SearchTerm); // Alle ansatte der matcher searchterm hentes, som default % (alle)
                return Page();
            }
            else
            {
                return RedirectToPage("./StaffLogin");
            }            
        }
    }
}
