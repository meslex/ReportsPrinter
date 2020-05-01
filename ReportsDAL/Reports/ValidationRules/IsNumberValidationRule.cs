using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Reports
{
    public class IsNumberValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var str = value as string;
            int result;
            try
            {
                result = Convert.ToInt32(value);
                if(result > 0)
                    return new ValidationResult(true, null);
                else
                    return new ValidationResult(false, "Значення від'ємне.");
            }
            catch (OverflowException)
            {
                return new ValidationResult(false, "Значення невірне."); 
            }
            catch (FormatException)
            {
                return new ValidationResult(false, "Невідомий формат вводу. Дозволенний формат: 41, 42, 64.");
            }
        }
    }
}
