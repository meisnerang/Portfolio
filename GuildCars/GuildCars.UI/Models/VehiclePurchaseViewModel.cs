using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class VehiclePurchaseViewModel : IValidatableObject
    {
        public Vehicle Vehicle { get; set; }
        public VehicleItem VehicleItem { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
        public SaleItem SaleItem { get; set; }
        public IEnumerable<SelectListItem> State { get; set; }
        public IEnumerable<SelectListItem> PurchaseType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(SaleItem.Name))
            {
                errors.Add(new ValidationResult("Name is required"));
            }
            if ((string.IsNullOrEmpty(SaleItem.Phone)) && (string.IsNullOrEmpty(SaleItem.Email)))
            {
                errors.Add(new ValidationResult("Either Phone or Email is required"));
            }
            if (string.IsNullOrEmpty(SaleItem.Street1))
            {
                errors.Add(new ValidationResult("Street is required"));
            }
            if (string.IsNullOrEmpty(SaleItem.City))
            {
                errors.Add(new ValidationResult("City is required"));
            }
            if (SaleItem.ZipCode <= 0)
            {
                errors.Add(new ValidationResult("ZipCode is required"));
            }
            if (SaleItem.PurchasePrice <= (decimal.Multiply(VehicleItem.SalePrice, .95M)))
            {
                errors.Add(new ValidationResult("Purchase Price cannot be less than . . ."));
            }
            if (SaleItem.PurchasePrice < VehicleItem.MSRP)
            {
                errors.Add(new ValidationResult("Purchase Price cannot exceed the MSRP"));
            }

            return errors;
        }
    }
}