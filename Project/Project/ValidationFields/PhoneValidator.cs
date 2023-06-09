using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project.ValidationFields
{
    class PhoneValidator : ValidationRule
    {
        private string _regex = @"^\+7-\d{3}-\d{3}-\d{2}-\d{2}$";
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value.ToString().Length == 0)
            {
                return new ValidationResult(false, "Введите свой номер телефона.");
            }
            else if (!Regex.IsMatch(value.ToString(), _regex))
            { 
                return new ValidationResult(false, "Введите номер по шаблону +7-xxx-xxx-xx-xx.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
