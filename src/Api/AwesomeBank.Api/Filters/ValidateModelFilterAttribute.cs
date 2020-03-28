namespace AwesomeBank.Api.Filters
{
    using System;
    using System.Linq;
    using AwesomeBank.Api.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateModelFilterAttribute : ActionFilterAttribute
    {
        private const string Message = "Model is invalid";
        private const string Code = "model_is_invalid";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorResponseViewModel = new ErrorResponseViewModel
                {
                    Message = Message,
                    Code = Code,
                    Errors = context.ModelState.Keys.SelectMany(
                        key => context.ModelState[key].Errors.Select(
                            x => new ErrorViewModel
                            {
                                Field = key,
                                Message = x.ErrorMessage
                            }))
                };

                context.Result = new BadRequestObjectResult(errorResponseViewModel);
            }
        }
    }
}