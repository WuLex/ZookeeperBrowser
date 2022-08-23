using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using yrjw.ORM.Chimp.Result;

namespace CoreAPI.Code.Middleware
{
    public class ValidateResultFormatHandler : IValidateResultFormatHandler
    {
        public void Format(ResultExecutingContext context)
        {
            var errors = context.ModelState
                .Where(m => m.Value.ValidationState == ModelValidationState.Invalid)
                .Select(m => new Errors
                {
                    Id = m.Key,
                    Msg = m.Value.Errors.Select(n => n.ErrorMessage).Aggregate((x, y) => x + ";" + y)
                }).ToList();

            context.Result = new JsonResult(ResultModel.Failed(errors));
        }
    }
}
