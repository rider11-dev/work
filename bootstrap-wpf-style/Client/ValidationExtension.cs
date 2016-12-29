using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class ValidationExtension
    {
        public static string ValidateProperty(this object obj, string propName, Type metadataType = null)
        {
            if (string.IsNullOrEmpty(propName))
            {
                return string.Empty;
            }

            var targetType = obj.GetType();
            if (metadataType != null && targetType != metadataType)
            {
                var provider = TypeDescriptor.GetProvider(targetType);
                TypeDescriptor.AddProviderTransparent(
                    new AssociatedMetadataTypeTypeDescriptionProvider(targetType, metadataType), targetType);
            }
            var propValue = targetType.GetProperty(propName).GetValue(obj);
            var validationContext = new ValidationContext(obj, null, null);
            validationContext.MemberName = propName;
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateProperty(propValue, validationContext, validationResults);

            if (validationResults.Count > 0)
            {
                return validationResults.First().ErrorMessage;
            }
            return string.Empty;
        }
    }
}
