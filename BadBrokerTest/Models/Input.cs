using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BadBrokerTest.Models
{
    public class Input
    {
        [Display(Name = "Date From")]
        [Required]
        [DateLesserThan("DateTill")]
        [RangeDate]
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }
        [Display(Name = "Date Till")]
        [Required]
        [DateGreaterThan("DateFrom")]
        [RangeDate]
        [DataType(DataType.Date)]
        public DateTime DateTill { get; set; }
        [Display(Name = "Amount, USD")]
        [Required]
        [Range(0.01d,double.MaxValue)]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
    }

    //This solution I got from StackOverFlow.com. Now it looks like a Workaround, but can be more elegant.
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DateGreaterThanAttribute : ValidationAttribute, IClientValidatable
    {
        readonly string _otherPropertyName;

        public DateGreaterThanAttribute(string otherPropertyName)
            : base("Date Till must be greater than Date From or Historical period cannot exceed 2 months")
        {
            _otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);
            if (otherProperty.PropertyType == new DateTime().GetType())
            {
                var thisPropertyValue = (DateTime)value;
                var otherPropertyValue = (DateTime)otherProperty.GetValue(validationContext.ObjectInstance, null);

                if (thisPropertyValue.CompareTo(otherPropertyValue) < 1 ||
                    thisPropertyValue.CompareTo(otherPropertyValue) > 61)
                {
                    return new ValidationResult(ErrorMessageString);
                }
            }
            else
            {
                return new ValidationResult(
                    "An error occurred while validating the property. OtherProperty is not of type DateTime");
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var dateGreaterThanRule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessageString,
                ValidationType = "dategreaterthan"
            };
            dateGreaterThanRule.ValidationParameters.Add("otherpropertyname", _otherPropertyName);

            yield return dateGreaterThanRule;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DateLesserThanAttribute : ValidationAttribute, IClientValidatable
    {
        readonly string _otherPropertyName;

        public DateLesserThanAttribute(string otherPropertyName)
            : base("Date From must be lesser than Date Till or Historical period cannot exceed 2 months")
        {
            _otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);
            if (otherProperty.PropertyType == new DateTime().GetType())
            {
                var thisPropertyValue = (DateTime)value;
                var otherPropertyValue = (DateTime)otherProperty.GetValue(validationContext.ObjectInstance, null);

                if (thisPropertyValue.CompareTo(otherPropertyValue) > -1 ||
                    otherPropertyValue.CompareTo(thisPropertyValue) > 61)
                {
                    return new ValidationResult(ErrorMessageString);
                }
            }
            else
            {
                return new ValidationResult(
                    "An error occurred while validating the property. OtherProperty is not of type DateTime");
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var dateLesserThanRule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessageString,
                ValidationType = "datelesserthan"
            };
            dateLesserThanRule.ValidationParameters.Add("otherpropertyname", _otherPropertyName);

            yield return dateLesserThanRule;
        }
    }

    public class RangeDateAttribute : RangeAttribute, IClientValidatable
    {
        public RangeDateAttribute()
            : base(typeof (DateTime),
                new DateTime(1999, 1, 4).ToShortDateString(),
                DateTime.Today.ToShortDateString())
        {}

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "validateage",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
            };

            rule.ValidationParameters.Add("minumumdate", new DateTime(1999, 1, 4).ToString("yyyy-MM-dd"));
            rule.ValidationParameters.Add("maximumdate", DateTime.Today.ToString("yyyy-MM-dd"));

            yield return rule;
        }
    }
}