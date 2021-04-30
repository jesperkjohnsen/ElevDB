using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ElevDB.Models;
using ElevDB.DataLogic;
using Microsoft.AspNetCore.Http;

namespace ElevDB.Pages.Staff
{
    public class StaffDetailModel : PageModel
    {
        public Models.Staff Staff { get; set; } // Er nødt til at kolde Models.Staff da jeg har været så klog at kalde mappen i pages staff også.

        public IActionResult OnGet(int staffId)
        {
            if(HttpContext.Session.GetString("Administration") == "TRUE") { // Der foretages login tjek
            Staff = StudentDatabase.GetStaffById(staffId); // Staff hentes fra databasen
            if (Staff == null) // Hvis der ikke kunne findes en ansat redirectes man tilbage til stafflist.
            {
                return RedirectToPage("./StaffList");
                }
            else { // Hvis der blev fundet en ansat vises detail siden.
                    return Page();
                }
            }
            else
            {
                return RedirectToPage("./StaffLogin");
            }
        }
    }
}
