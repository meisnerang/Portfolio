using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercises.Attributes
{
    public class IsDecimalAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(value is decimal)
            {
                return (bool)value;
            }
            return false;
        }
    }
}