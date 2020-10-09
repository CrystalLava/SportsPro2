﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SportsPro.Models;
using SportsPro.ViewModels;

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin, Tech")]
    public class TechIncidentController : Controller
    {
        private SportsProContext context { get; set; }

        public TechIncidentController(SportsProContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ViewResult Get()
        {
            var technicians = context.Technicians.ToList();
            return View(technicians);
        }

        [HttpGet]
        public IActionResult List(int id)
        {
            int? sessionID = HttpContext.Session.GetInt32("sessionID");
            if (id != 0)
            {

                var technician = context.Technicians.Find(id);
                var viewModel = new TechIncidentViewModel
                {
                    Technician = technician,
                    Incidents = context
                        .Incidents
                        .Include(i => i.Customer)
                        .Include(i => i.Product)
                        .Where(i => i.TechnicianID == technician.TechnicianID
                        && i.DateClosed == null)
                        .ToList()
                };
                if (viewModel.Incidents.Count == 0)
                    TempData["message"] = $"No open incidents for this Technicien";

                return View(viewModel);
            }
            else
            {
                TempData["message"] = $"Please enter a  Technicien";
                //return RedirectToActionResult ("get");
                return RedirectToAction("Get");
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var i = context.Incidents
                 .Include(i => i.Customer)
                .Include(i => i.Product)
                .Include(i => i.Technician)
                 .SingleOrDefault(i => i.IncidentID == id);
            return View(i);
        }


        [HttpPost]
        public IActionResult Edit(Incident incident)
        {

            var i = context.Incidents.Find(incident.IncidentID);
            i.Description = incident.Description;
            i.DateClosed = incident.DateClosed;
            HttpContext.Session.SetInt32("sessionID", (int)incident.TechnicianID);
            context.Incidents.Update(i);
            context.SaveChanges();

            return RedirectToAction("List", new { id = incident.TechnicianID });
        }

    }
}