using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class TechniciansController : Controller
    {
        private SportsProContext context { get; set; }

        public TechniciansController(SportsProContext context)
        {
            this.context = context;
        }

        [Route("Technicians")]
        public IActionResult Index()
        {
            var technicians = context.Technicians.ToList();
            return View(technicians);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Technician());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var t = context.Technicians.Find(id);
            return View(t);
        }

        [HttpPost]
        public IActionResult Edit(Technician t)
        {
            if (t.TechnicianID == 0)
                context.Technicians.Add(t);
            else
                context.Technicians.Update(t);

            context.SaveChanges();
            TempData["Success"] = "Success!";
            return RedirectToAction("Index", "Technicians");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete";
            var t = context.Technicians.Find(id);
            return View(t);
        }


        [HttpPost]
        public IActionResult Delete(Technician t)
        {
            ViewBag.Action = "";
            context.Technicians.Remove(t);
            context.SaveChanges();
            TempData["Success"] = "Success!";
            return RedirectToAction("Index", "Technicians");
        }

    }
}
