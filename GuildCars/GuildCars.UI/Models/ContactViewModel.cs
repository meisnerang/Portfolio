using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class ContactViewModel : IValidatableObject
    {
        public Contact Contact { get; set; }
        public string Message { get; set; }
        public VehicleItem Vehicle { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Contact.Name))
            {
                errors.Add(new ValidationResult("Name is required"));
            }
            if (string.IsNullOrEmpty(Contact.Message))
            {
                errors.Add(new ValidationResult("Message is required"));
            }
            if ((string.IsNullOrEmpty(Contact.Phone)) && (string.IsNullOrEmpty(Contact.Email)))
            {
                errors.Add(new ValidationResult("Either Phone or Email is required"));
            }

            return errors;
        }
    }

}