using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Inkukan.Api.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public sealed class AuthorizeOwnerOrAdminAttribute : Attribute, IAsyncActionFilter
{
    private readonly string _userIdParam;

    public AuthorizeOwnerOrAdminAttribute(string userIdParam = "userId")
        => _userIdParam = userIdParam;

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;

        if (!context.ActionArguments.TryGetValue(_userIdParam, out var raw)
            || raw is not Guid ownerId)
        {
            context.Result = new BadRequestObjectResult(
                $"Could not resolve '{_userIdParam}' from action arguments.");
            return;
        }

        var isAdmin = user.IsInRole("Admin");

        var currentUserId = Guid.TryParse(
            user.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            out var uid) ? uid : (Guid?)null;

        var isAllowed = isAdmin || currentUserId == ownerId;

        if (!isAllowed)
        {
            context.Result = new ForbidResult();
            return;
        }

        await next();
    }
}