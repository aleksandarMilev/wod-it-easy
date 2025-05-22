namespace WodItEasy.Common.Web.Extensions
{
    using Application;
    using Microsoft.AspNetCore.Mvc;

    public static class ResultExtensions
    {
        public static async Task<ActionResult<TData>> ToActionResult<TData>(
            this Task<TData> resultTask)
        {
            var result = await resultTask;

            if (result is null)
                return new NotFoundResult();

            return result;
        }

        public static async Task<ActionResult> ToActionResult(
            this Task<Result> resultTask)
        {
            var result = await resultTask;

            if (result.Succeeded)
                return new OkResult();

            return new BadRequestObjectResult(result.Errors);
        }

        public static async Task<ActionResult<TData>> ToActionResult<TData>(
            this Task<Result<TData>> resultTask)
        {
            var result = await resultTask;

            if (result.Succeeded)
                return result.Data;

            return new BadRequestObjectResult(result.Errors);
        }
    }
}
