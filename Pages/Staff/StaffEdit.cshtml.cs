using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevDB.DataLogic;
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
                RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}
