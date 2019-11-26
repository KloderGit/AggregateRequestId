﻿using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace AggregateRequestId
{
    public class AggregateRequestIdMiddleware
    {
        private readonly RequestDelegate _next;

        public AggregateRequestIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AggregateRequest aggregateRequest)
        {
            if (context.Request.Headers.Any(x => x.Key == "Aggregate-Request-Id"))
            {
                var id = context.Request.Headers.FirstOrDefault(x => x.Key == "Aggregate-Request-Id").Value;
                context.Response.Headers.Add("Aggregate-Request-Id", id);
            }
            else
            {
                context.Response.Headers.Add("Aggregate-Request-Id", aggregateRequest.AggregateRequestId.ToString());
            }

            await _next.Invoke(context);
        }
    }
}
