using System.Globalization;
using System.Windows.Controls;

namespace ValidationSample.ValidationRules
{
    class TextBoxValidationRule : ValidationRule
    {

        public int MinValue { get; set; }
        public int MaxValue { get; set; }


        public TextBoxValidationRule()
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
                return new ValidationResult(false, MinValue + "～" + MaxValue + "の範囲で数値を入力してください！！！");
            }

            return new ValidationResult(true, null);
            //            return ValidationResult.ValidResult;
        }
    }
}
