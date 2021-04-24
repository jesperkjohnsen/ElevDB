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
        public Models.Staff Staff { get; set; } // Er n�dt til at kolde Models.Staff da jeg har v�ret s� klog at kalde mappen i pages staff ogs�.

        public IActionResult OnGet(int staffId)
        {
            if(HttpContext.Session.GetString("Administration") == "TRUE") { 
            Staff = StudentDatabase.GetStaffById(staffId);
            if (Staff == null)
            {
                return RedirectToPage("./StaffList");
                }
                else { 
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
