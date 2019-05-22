using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercises.Models.Data
{
    public class Address
    {
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Please enter a street name")]
        public string Street1 { get; set; }

        public string Street2 { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please select a state")]
        public State State { get; set; }

        [Required(ErrorMessage = "Please enter a zipcode")]
        public string PostalCode { get; set; }
    }
}