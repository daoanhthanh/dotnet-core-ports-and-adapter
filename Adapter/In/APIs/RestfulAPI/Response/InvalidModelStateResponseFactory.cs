// using Microsoft.AspNetCore.Mvc;
// using be_aspnet_demo.models.exceptions;
//
// namespace Adapter.RestfulAPI.Response;
//
// public static class InvalidModelStateResponseFactory
// {
//     public static IActionResult ProduceErrorResponse(ActionContext context)
//     {
//         var errors = context.ModelState.SelectMany(m => m.Value!.Errors)
//             .Select(m => m.ErrorMessage)
//             .ToList();
//
//         var response = new ResourceError(messages: errors);
//
//         return new BadRequestObjectResult(response);
//     }
// }