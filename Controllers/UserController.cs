using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using login_registration.Models;

namespace login_registration.Controllers
{
    public class UserController : Controller
    {
        private readonly DbConnector _dbConnector;
 
        public UserController(DbConnector connect)
        {
            _dbConnector = connect;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [Route("register")]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                var email = _dbConnector.Query($"SELECT * FROM users WHERE email='{user.email}'");
                if(email.Count == 0)
                {
                    _dbConnector.Execute($"INSERT INTO users (firstname, lastname, email, password, created_at, updated_at) VALUES ('{user.firstname}', '{user.lastname}', '{user.email}', '{user.password}', NOW(), NOW())");
                    return RedirectToAction("Success");                    
                }
                else
                {
                    TempData["email_exists"] = "That email has already been registered";
                    return RedirectToAction("Index");
                }

            }
            else
            {
                return View("Index");                
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            var user = _dbConnector.Query($"SELECT * FROM users WHERE email='{email}'");
            if(user.Count > 0)
            {
                if((string)user[0]["password"] == password)
                {
                    return RedirectToAction("Success");
                }
                else
                {
                    TempData["passError"] = "Incorrect Password";  
                }
                
            }
            else
            {
                TempData["emailReg"] = "This email has not been registered";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            var AllUsers = _dbConnector.Query("SELECT * FROM users");
            ViewBag.allUsers = AllUsers;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
