using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using ZookeeperBrowser.Message;

namespace ZookeeperBrowser.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> logger;

        public HttpGlobalExceptionFilter(IHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            logger.LogError(
                new EventId(exception.HResult),
                exception,
                exception.Message);
            var errorResp = new ResponseMessage
            {
                Message = exception.Message,
                ErrorCode = "0001",
                IsSuccess = false
            };
            var result = new JsonResult(errorResp);
            result.StatusCode = (int)HttpStatusCode.OK;
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}