using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using demo_simple_ion_cms.Constants;

namespace demo_simple_ion_cms.Validators
{
    public class IndexesNormalCharsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is not List<string>)
            {
                ErrorMessage = "Tag names cannot be null";
                return false;
            }

            var indexes = (List<string>)value;

            foreach (var index in indexes)
            {
                var regex = new Regex("^[ A-Za-z]+$");
                
                if (!regex.IsMatch(index))
                {
                    ErrorMessage = ErrorConstants.ERR_VALIDATE_NORMAL_CHARS;
                    return false;
                }
            }

            return true;
        }
    }
}