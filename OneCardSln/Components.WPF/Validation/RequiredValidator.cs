using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Components.WPF.Validation
{
    public class RequiredValidator : Validator
    {
        public string FieldName { get; set; }

        public override string ErrorMessage { get { return FieldName + "不能为空值"; } }

        public override bool InitialValidation()
        {
            if (base.Source == null)
            {
                return false;
            }

            return string.IsNullOrEmpty(base.Source.ToString());
        }
    }
}
