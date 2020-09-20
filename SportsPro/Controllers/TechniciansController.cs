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

        [TempData]
        public string Message { get; set; }

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
                public RedirectToActionResult Edit(Technician technician)
                {
                    if (technician.TechnicianID == 0)
                    {
                        Message = $"Added Technician {technician.Name}";
                        context.Technicians.Add(technician);
                    }
                    else
                    {
                        Message = $"Edited Technician {technician.Name}";
                        context.Technicians.Update(technician);
                    }

                    context.SaveChanges();
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
        public IActionResult Delete(Technician technician)
        {
            Message = $"Deleted Technician {technician.Name}";
            context.Technicians.Remove(technician);
            context.SaveChanges();
            TempData["Success"] = "Success!";
            return RedirectToAction("Index", "Technicians");
        }

    }
}
