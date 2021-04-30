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
            if (HttpContext.Session.GetString("Administration") == "TRUE") // Der foretages login tjek
            {
                if (staffId.HasValue) // Hvis der koms staffId med hentes staff fra StudentDB
                {
                    staff = StudentDatabase.GetStaffById(staffId.Value);
                }
                else // Hvis ikke oprettes et nyt staff objekt
                {
                    staff = new Models.Staff();
                }
                if (staff == null) // Hvis staff er null på dette tidspunkt sendes man til notfound siden.
                {
                    return RedirectToPage("/NotFound");
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

            if (staff.staffId > 0) // Hvis staffId er højere end 0 er der tale om at en ansat skal opdateres
            {
                StudentDatabase.UpdateStaff(staff);
            }
            else // Ellers oprettes en ny ansat i databasen.
            {
                StudentDatabase.CreateStaff(staff);
            }
            TempData["Message"] = "Staff saved";
            return RedirectToPage("./StaffDetail", new { staffId = staff.staffId });
        }
    }
}
