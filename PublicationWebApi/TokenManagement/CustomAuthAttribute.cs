using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PublicationWebApi.TokenManagement
{
    public class CustomAuthAttribute : ActionFilterAttribute, IAsyncActionFilter
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var path = context.HttpContext.Request.Path.Value;
            var apiToken = string.Empty;

            if (context.HttpContext.Request.Query.TryGetValue("api-token", out var val))
                apiToken = val.ToString();

            try
            {
                var permissions = DumbApiTokenManager.TokensWithPermissions[apiToken];
                if (permissions.Contains(path))
                {
                    await next();
                    return;
                }
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            catch (KeyNotFoundException e)
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
            }
        }
    }
}
