using System.Net;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.API.Filters;

public class HttpExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is HttpResponseException httpResponseException && httpResponseException.Response.StatusCode == HttpStatusCode.NotFound)
        {
            context.Result = new NotFoundResult();
            context.ExceptionHandled = true;
        }
    }
}