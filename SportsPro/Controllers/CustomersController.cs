using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class CustomersController : Controller
    {
        private SportsProContext context { get; set; }

        public CustomersController(SportsProContext context)
        {
            this.context = context;
        }
        [TempData]
        public string Message { get; set; }

        [Route("Customers")]// Add Route
        public IActionResult Index()
        {
            var Customers = context.Customers.OrderBy(g => g.FirstName).ToList();
            return View(Customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Countries = context.Countries.OrderBy(g => g.Name).ToList();
            return View("Edit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Countries = context.Countries.OrderBy(g => g.Name).ToList();
            var t = context.Customers.Find(id);
            return View(t);
        }
        	
         [HttpPost]
         public RedirectToActionResult Edit(Customer customer)
         {
            if (customer.CustomerID == 0)
            {
               Message = $"Added Customer {customer.FullName}";
               context.Customers.Add(customer);
            }
            else
            {
               Message = $"Edited Customer {customer.FullName}";
               context.Customers.Update(customer);
            }

            context.SaveChanges();
            return RedirectToAction("Index", "Customers");
         }
       
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Action = "Delete";
            var t = context.Customers.Find(id);
            return View(t);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            Message = $"Deleted Customer {customer.FullName}";
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
    }
}
/*[10:09 AM] Hui Jillain
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

    public class CustomersController : Controller

    {

        private SportsProContext context { get; set; }         public CustomersController(SportsProContext context)

        {

            this.context = context;

        }

        [TempData]

        public string Message { get; set; }         [Route("Customers")]// Add Route

        public IActionResult Index()

        {

            ViewBag.Action = "Edit";

            var Customers = context.Customers.OrderBy(g => g.FirstName).ToList();

            return View(Customers);

        }         [HttpGet]

        public IActionResult Add()

        {

            ViewBag.Action = "Add";

            ViewBag.Countries = context.Countries.OrderBy(g => g.Name).ToList();

            return View("Edit", new Customer());

        }         [HttpGet]

        public IActionResult Edit()

        {

            ViewBag.Action = "Edit";

            ViewBag.Countries = context.Countries.OrderBy(g => g.Name).ToList();

            //var t = context.Customers.Find(id);

            //return View(t);

            return View();

        }

         [HttpPost]

         public IActionResult Edit(Customer customer)

         {

            //if (ModelState.IsValid)

            //{

            //    if (customer.CustomerID == 0)

            //    {

            //        Message = $"Added Customer {customer.FullName}";

            //        context.Customers.Add(customer);

            //    }

            //    else

            //    {

            //        Message = $"Edited Customer {customer.FullName}";

            //        context.Customers.Update(customer);

            //    }             //    context.SaveChanges();

            //    return RedirectToAction("Index", "Customers");

            //}

            //else

            //{

            //    ViewBag.Action = (customer.CustomerID == 0) ? "Add" : "Edit";

            //    ViewBag.Countries = context.Countries.OrderBy(g => g.Name).ToList();

            //    return View(customer);

            //}             if (TempData["okEmail"] == null)

            {

                string msg = Check.EmailExists(context, customer.Email);

                if (!String.IsNullOrEmpty(msg))

                {

                    ModelState.AddModelError(nameof(Customer.Email), msg);

                }

            }

            if (ModelState.IsValid)

            {

                context.Customers.Add(customer);

                context.SaveChanges();

                return RedirectToAction("Welcome");

            }

            else return View(customer);

         }

        [HttpGet]

        public IActionResult Delete(int id)

        {

            ViewBag.Action = "Delete";

            var t = context.Customers.Find(id);

            return View(t);

        }         [HttpPost]

        public IActionResult Delete(Customer customer)

        {

            Message = $"Deleted Customer {customer.FullName}";

            context.Customers.Remove(customer);

            context.SaveChanges();

            return RedirectToAction("Index", "Customers");

        }

    }

}

[10:10 AM] Hui Jillain
using System;

using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.ModelBinding; namespace SportsPro.Models

{

    public class Customer

    {

        public int CustomerID { get; set; }         [Required]

        [Range(1, 51, ErrorMessage = "First Name must be between 1 and 51 characters.")]

        public string FirstName { get; set; }         [Required]

        [Range(1, 51, ErrorMessage = "Last Name must be between 1 and 51 characters.")]

        public string LastName { get; set; }         [Required]

        [Range(1, 51, ErrorMessage = "Address must be between 1 and 51 characters.")]

        public string Address { get; set; }         [Required]

        [Range(1, 51, ErrorMessage = "City must be between 1 and 51 characters.")]

        public string City { get; set; }         [Required]

        [Range(1, 51, ErrorMessage = "State must be between 1 and 51 characters.")]

        public string State { get; set; }         [Required]

        [Range(1, 21, ErrorMessage = "Postal Code must be between 1 and 21 characters.")]

        public string PostalCode { get; set; }         [Required]

        public string CountryID { get; set; }

        public Country Country { get; set; }         [RegularExpression(@"^((\+0?1\s)?)\(?\d{3}\)?[\s.\s]\d{3}[\s.-]\d{4}$")]

        public string Phone { get; set; }         [Required(ErrorMessage = "Please enter a valid email address.")]

        [Remote("CheckEmail", "Validation")]

        [Range(1, 51, ErrorMessage = "Email must be between 1 and 51 characters.")]

        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }         public string FullName => FirstName + " " + LastName;   // read-only property

    }

}

*/
