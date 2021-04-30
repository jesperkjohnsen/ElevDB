using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ElevDB.DataLogic;
using ElevDB.Models;
using Microsoft.AspNetCore.Http;

namespace ElevDB.Pages.Staff
{
    public class StaffLoginModel : PageModel
    {
        [BindProperty]
        public Models.Staff LoginStaff { get; set; }
        [TempData]
        public string LoginMessage { get; set; }
        private Models.Staff staff { get; set; }

        public void OnGet()
        {
        }
        
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {                
                staff = StudentDatabase.Login(LoginStaff.Username,LoginStaff.Password);
                if (staff.staffId != 0)
                {
                    if (staff.Administration == true) // hvis brugeren hentet fra SQL er i administration, så sættes det til True i session.
                    {
                        HttpContext.Session.SetString("Administration", "TRUE");
                    }
                    if (staff.Teacher == true) // hvis brugeren hentet fra SQL er underviser, så sættes det til True i session.
                    {
                        HttpContext.Session.SetString("Teacher", "TRUE");
                    }
                    HttpContext.Session.SetString("LoggedIn", "TRUE");
                    return RedirectToPage("/Students/StudentList"); // Efter login foretages redirect til studentlist.
                }
                else
                {
                    TempData["LoginMessage"] = "Invalid login"; // Hvis login fejler, sættes en midlertidig besked som vises på login siden bagefter.
                    return Page();
                }

            }
        }
    }        
}