using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TinyLibrary.WebApi.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var actionArgument in context.ActionArguments.Values)
            {
                if (actionArgument == null)
                {
                    continue;
                }

                var validatorType = typeof(IValidator<>).MakeGenericType(actionArgument.GetType());
                if (context.HttpContext.RequestServices.GetService(validatorType) is not IValidator validator) continue;


                var validationContext = new ValidationContext<object>(actionArgument);
                var result = await validator.ValidateAsync(validationContext);

                if (!result.IsValid)
                {
                    var errors = result.Errors.GroupBy(e => e.PropertyName).ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                    context.Result = new BadRequestObjectResult(new ValidationProblemDetails(errors)
                    {
                        Status = StatusCodes.Status400BadRequest
                    });

                    return;
                }
            }

            await next();
        }
    }
}
