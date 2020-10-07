using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SportsPro.Models
{
    public class RegisterViewModel
    {
        private List<Customer> customers;
        public List<Customer> Customers
        {
            get => customers;
            set
            {
                customers = value;
            }
        }
        public string FilterString { get; set; }
        public string ActiveIncident { get; set; }
        public string ActiveTechnician { get; set; }
        public string ActiveCustomer { get; set; }
        public string Action { get; set; }

        public Incident CurrentIncident { get; set; }

        public Customer CurrentCustomer { get; set; }

        public Product CurrentProduct { get; set; }

        public int? CustomerID { get; set; }

        public string WarningText { get; set; }

        private List<Product> products;

        public List<Product> Products
        {
            get => products;
            set
            {
                products = value;
            }
        }

        private List<Product> customerProducts;

        public List<Product> CustomerProducts
        {
            get => customerProducts;
            set
            {
                customerProducts = value;
            }
        }

        private List<Incident> incidents;

        public List<Incident> Incidents
        {
            get => incidents;
            set
            {
                incidents = value;
            }
        }

        public int? TechnicianID { get; set; }

        private List<Technician> technicians;
        public List<Technician> Technicians
        {
            get => technicians;
            set
            {
                technicians = value;
            }
        }
        public string CheckActiveIncident(string i) =>
            i.ToLower() == ActiveIncident.ToLower() ? "active" : "";
        public string CheckActiveTechnician(string t) =>
            t.ToLower() == ActiveTechnician.ToLower() ? "active" : "";

    }
}
