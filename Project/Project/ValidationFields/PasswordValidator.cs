using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project.ValidationFields
{
    class PasswordValidator : ValidationRule
    {
        private string _regex = @"[A-Za-z0-9\W]+$";

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value.ToString().Length == 0)
            {
                return new ValidationResult(false, "Необходимо указать пароль.");
            }
            else if (!Regex.IsMatch(value.ToString(), _regex)) 
            { 
                    return new ValidationResult(false, "Пароль содержит запрещённые символы.");
            }
            else if (value.ToString().Length < 6)
            {
                return new ValidationResult(false, "Пароль слишком короткий, меньше 6 символов.");
            }
            else if (value.ToString().Length > 50)
            {
                return new ValidationResult(false, "Укажите более короткий пароль, до 50 символов.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
