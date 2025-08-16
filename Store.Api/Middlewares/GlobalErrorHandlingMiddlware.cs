using Domain.Exceptions;
using Shared.ErrorsModels;

namespace Store.Api.Middlewares
{
    public class GlobalErrorHandlingMiddlware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddlware> _logger;

        public GlobalErrorHandlingMiddlware(RequestDelegate next , ILogger<GlobalErrorHandlingMiddlware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next.Invoke(context);
                if (context.Response.StatusCode == StatusCodes.Status400BadRequest)
                {
                    await HandlingNotFoundEndPointAsync(context);

                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                await HandlingErrorAsync(context, ex);

            }


        }

        private static async Task HandlingErrorAsync(HttpContext context, Exception ex)
        {
            //1.Set Status Code For Response
            //2.Set Content Type Code For Response
            //3. Response Object (Body)
            //4. Return Response


            //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                ErrorMessage = ex.Message

            };
            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                ValidationEciption => HandlingValidationEciptionAsync((ValidationEciption)ex , response),



                _ => StatusCodes.Status500InternalServerError
            };
            context.Response.StatusCode = response.StatusCode;


            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandlingNotFoundEndPointAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = $"End Point {context.Request.Path} is not Found"
            };
            await context.Response.WriteAsJsonAsync(response);
        }


        private static int HandlingValidationEciptionAsync( ValidationEciption ex,ErrorDetails response )
        {
            response.Errors = ex.Errors;
            return StatusCodes.Status400BadRequest;

        }

    }
}
