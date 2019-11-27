using Microsoft.AspNetCore.Http;
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
                var id = aggregateRequest.AggregateRequestId.ToString();
                context.Request.Headers.Add("Aggregate-Request-Id", id);
                context.Response.Headers.Add("Aggregate-Request-Id", id);
            }

            await _next.Invoke(context);
        }
    }
}
