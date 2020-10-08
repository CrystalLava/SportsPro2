using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SportsPro.Controllers
{
    public class ValidationController : Controller
    {
        private SportsProContext context;
        //public CustomersController(SportsProContext ctx) => context = ctx;
        //public JsonResult CheckEmail(string email)
        //{
        //    bool hasEmail = Utility.CheckEmail(email);  //checks database
        //    if (hasEmail)
        //        return Json($"Email address {email} is already registered.");
        //    else
        //        return Json(true);
        //}
        public IActionResult Index()
        {
            return View();
        }
    }
}
