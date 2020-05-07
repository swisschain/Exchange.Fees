using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Fees.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

namespace Fees.Exceptions
{
    internal class UnhandledExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        const string MessageTemplate = "HTTP {RequestMethod} {RequestPath} {StatusCode} finished in {Elapsed:0.0000} ms";

        public UnhandledExceptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string body = null;
            var sw = Stopwatch.StartNew();
            try
            {
                body = await context.Request.GetBodyAsync();
                await _next.Invoke(context);
            }
            catch (EntityException ex)
            {
                sw.Stop();
                await ErrorResponse(context, (int)HttpStatusCode.OK, ex.ErrorCode, ex.Message, ex.Fields);
                return;
            }
            catch (Exception ex)
            {
                sw.Stop();
                var logger = context.GetEnrichLogger(body);
                logger.Error(ex, ex.Message);
                await ErrorResponse(context, (int)HttpStatusCode.InternalServerError, ErrorCode.RuntimeError, "Runtime error");
                return;
            }

            sw.Stop();
            context.GetEnrichLogger(body).Information(MessageTemplate, context.Request.Method, context.Request.Path, context.Response.StatusCode, sw.Elapsed.TotalMilliseconds);
        }

        private Task ErrorResponse(HttpContext ctx, int statusCode, ErrorCode errorCode, string message, Dictionary<string, string> fields = null)
        {
            ctx.Response.ContentType = "application/json";
            ctx.Response.StatusCode = statusCode;

            var response = ResponseModel.Fail((int)errorCode, message, fields ?? new Dictionary<string, string>());
            return ctx.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }

    internal static class HttpExtensions
    {
        public static async Task<string> GetBodyAsync(this HttpRequest request)
        {
            if (request.Method != "POST")
                return null;

            request.EnableBuffering();
            string body;

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                body = await reader.ReadToEndAsync();
            }

            request.Body.Seek(0, SeekOrigin.Begin);

            return body;
        }

        public static ILogger GetEnrichLogger(this HttpContext context, string body)
        {
            // TODO: Investigate why we don't have 'tenant-id' at this point
            var brokerId = "none"; //context.User.GetTenantId();

            var logger = Log
                .ForContext("BrokerId", brokerId);

            if (!string.IsNullOrWhiteSpace(body))
                logger = logger.ForContext("RequestBody", body);

            return logger;
        }
    }
}
