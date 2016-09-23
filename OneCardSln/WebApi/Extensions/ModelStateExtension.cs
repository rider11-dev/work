using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.ModelBinding;

namespace OneCardSln.WebApi.Extensions
{
    public static class ModelStateExtension
    {
        public static string Parse(this ModelStateDictionary stateDict)
        {
            if (stateDict.IsValid)
            {
                return string.Empty;
            }
            var errorStates = stateDict.Where(s => s.Value.Errors.Count > 0).ToList();
            StringBuilder sb = new StringBuilder();

            foreach (var state in errorStates)
            {
                sb.AppendFormat("{0}:", state.Key);
                foreach (var error in state.Value.Errors)
                {
                    sb.AppendFormat("{0},", error.ErrorMessage);
                }
                sb.Append(System.Environment.NewLine);
            }
            return sb.ToString().TrimEnd(',');
        }
    }
}