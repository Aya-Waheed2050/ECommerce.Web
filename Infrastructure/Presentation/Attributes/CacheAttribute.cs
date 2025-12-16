using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace Presentation.Attributes
{
    class CacheAttribute(int DurationInSec =90) : ActionFilterAttribute
    {

        public override async Task OnActionExecutionAsync(ActionExecutingContext context ,ActionExecutionDelegate next)
        {
            // create cash key
            string cashKey = CreateCashKey(context.HttpContext.Request);

            // search for value with cashKey
            ICacheService cashService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            string? cashValue = await cashService.GetAsync(cashKey);

            //return value if not null
            if(cashValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cashValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }
            ActionExecutedContext? executedContext = await next.Invoke();

            if (executedContext.Result is OkObjectResult result)
                await cashService.SetAsync(cashKey , result.Value ,TimeSpan.FromSeconds(DurationInSec));
        }


        private string CreateCashKey(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();
            key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(Q => Q.Key))
            {
                key.Append($"{item.Key}={item.Value}");
            }
            return key.ToString();
        }

    }
}
