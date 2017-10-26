using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using logreg.Models;
using DbConnection;

namespace logreg.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/reg/")]
        public IActionResult Register(Index user)
        {
            Reg newu = user.regdetails;
            TryValidateModel(newu);
            if(ModelState.IsValid)
            {
                List<Dictionary<string, object>> validusers = DbConnector.Query($"SELECT * FROM users WHERE email='{newu.email}'");
                if(validusers.Count < 1)
                {
                    DbConnector.Execute($"INSERT INTO users (firstname, lastname, email, password) VALUES ('{newu.fname}', '{newu.lname}', '{newu.email}', '{newu.pw}')");
                    Index nuser = new Index{
                        logdetails = new Log{
                            email=newu.email,
                            pw=newu.pw
                        }
                    };
                    return Login(nuser);
                }
                ViewBag.regerr = "This email is already taken. Try logging in.";
            }
            return View("Index");
        }

        [HttpPost]
        [Route("/log/")]
        public IActionResult Login(Index user)
        {
            Log loggin = user.logdetails;
            TryValidateModel(loggin);
            if(ModelState.IsValid)
            {
               List<Dictionary<string, object>> validusers = DbConnector.Query($"SELECT * FROM users WHERE email='{loggin.email}' AND password='{loggin.pw}'");
                if(validusers.Count > 0)
                {
                    return RedirectToAction("Success");
                }
                ViewBag.logerr = "This email-password combination does not exist. Try again or register.";
            }
            return View("Index");
        }

        [HttpGet]
        [Route("/success/")]
        public IActionResult Success()
        {
            return View();
        }
    }
}
