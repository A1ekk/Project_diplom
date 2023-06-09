using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project.ValidationFields
{
    class PatronymicValidator : ValidationRule
    {
        private string _regex = @"^([А-Яа-я])(?=[А-Яа-я]+$)";
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if ((!Regex.IsMatch(value.ToString(), _regex)) && (value.ToString().Length > 0))
            {
                return new ValidationResult(false, "Отчество должно состоять из кириллицы.");
            }
            else if (value.ToString().Length > 50)
            {
                return new ValidationResult(false, "Укажите более короткий вариант, до 50 символов.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
