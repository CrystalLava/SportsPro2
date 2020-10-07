﻿using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SportsPro.Models
{
    public class Customer
    {
		public int CustomerID { get; set; }

		[Required(ErrorMessage = "First Name must be between 1 and 51 characters.")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name must be between 1 and 51 characters.")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Address must be between 1 and 51 characters.")]
		public string Address { get; set; }

		[Required(ErrorMessage = "City must be between 1 and 51 characters.")]
		public string City { get; set; }

		[Required(ErrorMessage = "State must be between 1 and 51 characters.")]
		public string State { get; set; }

		[Required(ErrorMessage = "Postal Code must be between 1 and 21 characters.")]
		public string PostalCode { get; set; }

		[Required]
		public string CountryID { get; set; }
		public Country Country { get; set; }

		[RegularExpression(@"^((\+0?1\s)?)\(?\d{3}\)?[\s.\s]\d{3}[\s.-]\d{4}$",
			ErrorMessage = "Phone number must be in the (999) 999-9999 format.")]
		public string Phone { get; set; }

		[Required(ErrorMessage = "Please enter a valid email address.")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		public string FullName => FirstName + " " + LastName;   // read-only property
	}
}