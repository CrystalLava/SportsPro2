using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    //[Authorize(Roles = "Admin")]//Only Admin has access to this page
    public class TechniciansController : Controller
    {
        private SportsProContext context { get; set; }

        public TechniciansController(SportsProContext context)
        {
            this.context = context;
        }

        [TempData]//Temp message at top of page based on action taken
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
       [HttpPost]//Message when Technician Edited or added, save changes, and return to customer page
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


        [HttpPost]//Message when tech deleted, save changes, and return to tech page
        public IActionResult Delete(Technician technician)
        {
            Message = $"Deleted Technician {technician.Name}";
            context.Technicians.Remove(technician);
            context.SaveChanges();
            return RedirectToAction("Index", "Technicians");
        }

    }
}
