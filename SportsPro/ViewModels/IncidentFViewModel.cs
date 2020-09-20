using SportsPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.ViewModels
{
    public class IncidentFViewModel
    {
        public List<Incident> Incidents { get; set; }
        public string Filter { get; set; }
    }
}
