using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext context)
        {
            IEnumerable<ValidationError> Errors = context.ModelState
                                .Where(error => error.Value.Errors.Any())
                               .Select(error => new ValidationError()
                               {
                                   Field = error.Key,
                                   Errors = error.Value.Errors.Select(error => error.ErrorMessage)
                               });
            var response = new ValidationErrorResponse()
            {
                Errors = Errors,
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "One Or More Validation Error Happened"
            };
            return new BadRequestObjectResult(response);
        }
    }

}
