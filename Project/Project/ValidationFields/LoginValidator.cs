using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project.ValidationFields
{
    class LoginValidator : ValidationRule
    {
        private string _regex = @"^([A-Za-z])";
        private string _regex1 = @"^([A-Za-z])(?=[A-Za-z0-9]+$)";
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value.ToString().Length == 0)
            {
                return new ValidationResult(false, "Необходимо указать логин.");
            }
            else if (!Regex.IsMatch(value.ToString(), _regex))
            {
                return new ValidationResult(false, "Логин должен начинаться с латиницы.");
            }
            else if ((!Regex.IsMatch(value.ToString(), _regex1)) && (value.ToString().Length > 1))
            {
                return new ValidationResult(false, "Логин должен состоять из латиницы и цифр.");
            }
            else if (value.ToString().Length > 50)
            {
                return new ValidationResult(false, "Укажите более короткий логин, до 50 символов.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
