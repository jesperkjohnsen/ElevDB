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
    public class StaffEditModel : PageModel
    {
        [BindProperty]
        public Models.Staff staff { get; set; }

        public IActionResult OnGet(int? staffId)
        {
            if (HttpContext.Session.GetString("Administration") == "TRUE")
            {
                if (staffId.HasValue)
                {
                    staff = StudentDatabase.GetStaffById(staffId.Value);
                }
                else
                {
                    staff = new Models.Staff();
                }

                if (staff == null)
                {
                    return RedirectToPage("./NotFound");
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

            if (staff.staffId > 0)
            {
                StudentDatabase.UpdateStaff(staff);
            }
            else
            {
                StudentDatabase.CreateStaff(staff);
            }
            TempData["Message"] = "Staff saved";
            return RedirectToPage("./StaffDetail", new { staffId = staff.staffId });
        }

    }
}
