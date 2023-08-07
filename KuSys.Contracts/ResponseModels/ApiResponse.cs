using Microsoft.AspNetCore.Mvc;

namespace KuSys.Contracts.ResponseModels;

/// <summary>
/// Creates api response object depends on the data
/// </summary>
public static class ApiResponse

{
    /// <summary>
    /// Creates a ApiResponse with provided data. If operation was successful it will create OK(200) response otherwise BadRequest(400) response
    /// </summary>
    /// <param name="response">Data to return.</param>
    /// <typeparam name="T">Type of the data</typeparam>
    /// <returns>If operation was successful, 200 Ok response, otherwise 400 BadRequest response with provided data.</returns>
    public static IActionResult WithData<T>(T response)
        where T : BaseResponse
    {
        if (response.IsSuccess)
            return new OkObjectResult(response);
        return new BadRequestObjectResult(response);
    }
    
    /// <summary>
    /// Creates a ApiResponse with provided data. If operation was successful it will create NoContent(204) response otherwise BadRequest(400) response
    /// </summary>
    /// <param name="response">Data to return.</param>
    /// <typeparam name="T">Type of the data</typeparam>
    /// <returns>If operation was successful, 204 NoContent response, otherwise 400 BadRequest response with provided data.</returns>
    public static IActionResult Deleted<T>(T response)
        where T : BaseResponse
    {
        if (response.IsSuccess)
            return new NoContentResult();
        return new BadRequestObjectResult(response);
    }
    
    /// <summary>
    /// Creates a ApiResponse with provided data. If operation was successful it will create NoContent(204) response otherwise BadRequest(400) response
    /// </summary>
    /// <param name="response">Data to return.</param>
    /// <typeparam name="T">Type of the data</typeparam>
    /// <returns>If operation was successful, 204 NoContent response, otherwise 400 BadRequest response with provided data.</returns>
    public static IActionResult Updated<T>(T response)
        where T : BaseResponse
    {
        if (response.IsSuccess)
            return new NoContentResult();
        return new BadRequestObjectResult(response);
    }

    /// <summary>
    /// Creates an ApiResponse with provided data. If operation was successful it will create Created(201) response otherwise BadRequest(400) response
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="response">Data to return</param>
    /// <typeparam name="T">Type of the data</typeparam>
    /// <returns>If operation was successful, 201 Created response, otherwise 400 BadRequest response with provided data.</returns>
    public static IActionResult Created<T>(ControllerBase controller, T response)
        where T : BaseResponse
    {
        if (response.IsSuccess)
        {
            string actionName = controller.ControllerContext.ActionDescriptor.ActionName;
            string controllerName = controller.ControllerContext.ActionDescriptor.ControllerName;
            var routeValues = controller.ControllerContext.RouteData.Values;

            var createdAtActionResult = new CreatedAtActionResult(actionName, controllerName, routeValues, response);

            return createdAtActionResult;
        }


        return new CreatedResult("", response);
    }
}