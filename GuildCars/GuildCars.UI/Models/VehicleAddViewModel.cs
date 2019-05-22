using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class VehicleAddViewModel : IValidatableObject
    {
        public Vehicle Vehicle { get; set; }
        public VehicleItem VehicleItem { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
        public IEnumerable<SelectListItem> Make { get; set; }
        public IEnumerable<SelectListItem> Model { get; set; }
        public IEnumerable<SelectListItem> BodyStyle { get; set; }
        public IEnumerable<SelectListItem> Color { get; set; }
        public IEnumerable<SelectListItem> Interior { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            var currentDate = DateTime.Now.Year;
            var currentYearPlusOne = currentDate + 1;

            if (Vehicle.Year < 2000)
            {
                errors.Add(new ValidationResult("We can't accept cars older than year 2000"));
            }

            if (Vehicle.Year > currentYearPlusOne)
            {
                errors.Add(new ValidationResult("Year cannot be greater than next year"));
            }

            if ((Vehicle.IsNew) && (((Vehicle.Mileage < 0) || (Vehicle.Mileage > 1000))))
            {
                errors.Add(new ValidationResult("Mileage for new cars must range from 0 to 1000"));
            }
            
            if ((!Vehicle.IsNew) && (Vehicle.Mileage < 1000))
            {
                errors.Add(new ValidationResult("Mileage for used cars must be more than 1000"));
            }

            if ((string.IsNullOrEmpty(Vehicle.Vin)) || (Vehicle.Vin.Length != 17))
            {
                errors.Add(new ValidationResult("Please enter a valid 17 digit Vin"));
            }

            if (Vehicle.MSRP < 0)
            {
                errors.Add(new ValidationResult("MSRP must be positive"));
            }
            if (Vehicle.SalePrice < 0)
            {
                errors.Add(new ValidationResult("SalePrice must be positive"));
            }
            if (Vehicle.SalePrice > Vehicle.MSRP)
            {
                errors.Add(new ValidationResult("SalePrice cannot be more than MSRP"));
            }
            if (string.IsNullOrEmpty(Vehicle.Description))
            {
                errors.Add(new ValidationResult("Please enter a description"));
            }
            
            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                var extensions = new string[] { ".jpg", ".png", ".JPG", ".jpeg" };

                var extension = Path.GetExtension(ImageUpload.FileName);

                if (!extensions.Contains(extension))
                {
                    errors.Add(new ValidationResult("Image file must be a jpg, png, gif, or jpeg."));
                }
            }
            else
            {
                errors.Add(new ValidationResult("Image file is required"));
            }

            return errors;
        }
    }
}