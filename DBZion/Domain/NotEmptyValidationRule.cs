﻿using System.Globalization;
using System.Windows.Controls;
using DBZion;

namespace DBZion.Domain
{
    public class NotEmptyValidationRule: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, "Обязательное поле.")
                : ValidationResult.ValidResult;
        }
    }
}
