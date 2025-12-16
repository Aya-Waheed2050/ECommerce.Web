using Domain.Exceptions;
using Domain.Exceptions.NotFoundExceptions;
using Shared.ErrorModels;

namespace E_Commerce.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate next ,ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandelNotFoundEndPointAsync(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Some Thing Went Wrong => {ex.Message}");
                await HandelExceptionAsync(httpContext, ex);
            }
        }


        #region Helper Method

        private static async Task HandelExceptionAsync(HttpContext httpContext, Exception ex)
        {
            // Response Object
            var response = new ErrorDetails()
            {
                ErrorMessage = ex.Message
            };

            // StatusCode for Request
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetBadRequestError(badRequestException , response),
                _ => StatusCodes.Status500InternalServerError
            };
            // Return Object as Json
            response.StatusCode = httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(response); // ContentType = "Application/Json";
        }

        private static int GetBadRequestError(BadRequestException badRequestException, ErrorDetails response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandelNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorDetails()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} Is Not Found!"
                };
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        } 

        #endregion

    }
}
