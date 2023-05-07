using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Infrastructure.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ModelState.IsValid is false)
        {
           var errors = context.ModelState
                .Where(_modelState => _modelState.Value.Errors.Any())
                .ToDictionary(_modelState => _modelState.Key, _modelState => _modelState.Value.Errors.Select(_data => _data.ErrorMessage))
                .ToHashSet();

            context.Result = new BadRequestObjectResult(errors);

            return;
        }

        await next();
    }
}
