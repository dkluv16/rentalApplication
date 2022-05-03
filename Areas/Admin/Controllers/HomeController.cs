using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CampChetekRental.Models;
using Microsoft.AspNetCore.Http;

namespace CampChetekRental.Areas.Admin.Controllers
{
    [Route("[area]/[controller]/[action]")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private RentalAttribute context { get; set; }
       
        public HomeController(RentalAttribute ctx)
        {
            context = ctx;
        }
        
        [Route("{UserEmail?}")]
        [Route("{UserPassword}")]
        public IActionResult Login(string UserEmail, string UserPassword)
        {

            int userRole = 0;
            int Id = 0;
            bool isPasswordMatched = false;
            var userInfo = context.users.Where(u => u.Email == UserEmail);
            foreach (var login in userInfo)
            {
                isPasswordMatched = RentalAttribute.VerifyPassword(UserPassword, login.Hash, login.Salt);
                userRole = login.userRoleId;
                Id = login.userId;
            }
            if(isPasswordMatched == true && userRole != 4)
            {
                var session = new LoginSession(HttpContext.Session);
                session.SetId(Id.ToString());
                session.SetName(UserEmail.ToUpper());
                session.SetUserRole(userRole.ToString());
                return RedirectToAction("Dashboard", "Events");       
            }
           
            else
            {
                HttpContext.Session.Clear();
                return View();
            }

        }
        
        [HttpGet]
        public IActionResult CreateNew()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNew(User user)
        {
            if (ModelState.IsValid)
            {
                context.users.Add(RentalAttribute.CreateNewUser(0, user.Password, user.FirstName, user.LastName, user.Email, user.userRoleId = 4));
                context.SaveChanges();
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
           
        }
        
    }
}