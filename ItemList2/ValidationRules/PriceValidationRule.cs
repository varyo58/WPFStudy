using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ItemList2.ValidationRules
{
    public class PriceValidationRule : ValidationRule
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }


        public PriceValidationRule()
        {
            //既定値
            MinValue = int.MinValue;
            MaxValue = int.MaxValue;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "値がNullです");
            }
            int inputNum;
            if (!int.TryParse(value.ToString(), out inputNum))
            {
                return new ValidationResult(false, "値の形式が不正です");
            }

            if (inputNum < MinValue || MaxValue < inputNum)
            {
                return new ValidationResult(false, "値が範囲外です");
            }

            return new ValidationResult(true, null);
//            return ValidationResult.ValidResult;
        }
    }
}
