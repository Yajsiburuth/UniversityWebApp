using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace UniversityWebApp.Helper
{
    internal static class ErrorHelper
    {
        internal static Dictionary<string, string[]> ModelStateErrorsToDict(ModelStateDictionary modelState)
        {
            var errors = new Dictionary<string, string[]>();
            errors = modelState.Where(elem => elem.Value.Errors.Any())
                               .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => string.IsNullOrEmpty(e.ErrorMessage) ? e.Exception.Message : e.ErrorMessage)
                               .ToArray());
            return errors;
        }
    }
}