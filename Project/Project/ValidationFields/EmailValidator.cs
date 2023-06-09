using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project.ValidationFields
{
    class EmailValidator : ValidationRule
    {
        string _regex = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value.ToString().Length == 0)
            {
                return new ValidationResult(false, "Введите свой e-mail.");
            }
            else if (!Regex.IsMatch(value.ToString(), _regex)) 
            { 
                    return new ValidationResult(false, "Введите e-mail по шаблону xxxx@xxxx.xx.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
