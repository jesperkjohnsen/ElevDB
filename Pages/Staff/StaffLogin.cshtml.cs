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
                    if (staff.Administration == true)
                    {
                        HttpContext.Session.SetString("Administration", "TRUE");
                    }
                    if (staff.Teacher == true)
                    {
                        HttpContext.Session.SetString("Teacher", "TRUE");
                    }
                    HttpContext.Session.SetString("LoggedIn", "TRUE");

                    return RedirectToPage("/Students/StudentList");
                }

                else
                {
                    TempData["LoginMessage"] = "Invalid login";
                    return Page();
                }

            }
        }
    }        
}