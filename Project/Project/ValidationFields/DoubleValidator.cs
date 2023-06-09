using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project.ValidationFields
{
    class DoubleValidator : ValidationRule
    {
        private string _regex = @"^[\d]+,?[\d]*$";
        //`!@#$%&*()_=+[]{};:\|,.
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value.ToString().Length == 0)
            {
                return new ValidationResult(false, "Введите число.");
            }
            else if (!Regex.IsMatch(value.ToString(), _regex))
            {
                return new ValidationResult(false, "Некорректный формат.");
            }
            else if (value.ToString().Length > 50)
            {
                return new ValidationResult(false, "Сократите число.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
