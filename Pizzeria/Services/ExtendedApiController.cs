namespace Pizzeria.Services
{
    using Contracts.Result;
    using Microsoft.AspNetCore.Mvc;

    public class ExtendedApiController : ControllerBase
    {
        protected IActionResult OkOrError<T>(Result<T> result)
        {
            IActionResult errorResponse = GetErrorResponse(result);

            if (errorResponse != null)
            {
                return errorResponse;
            }

            return Ok(result);
        }

        protected IActionResult OkOrError(ResultCommonLogic result)
        {
            IActionResult errorResponse = GetErrorResponse(result);

            if (errorResponse != null)
            {
                return errorResponse;
            }

            return Ok();
        }

        private IActionResult GetErrorResponse(ResultCommonLogic result)
        {
            if (result.IsFailure)
            {
                var errorResponse = new ObjectResult(result.Message)
                {
                    StatusCode = (int)result.HttpStatusCode
                };

                return errorResponse;
            }

            return null;
        }
    }
}
