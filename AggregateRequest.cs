using System;

namespace AggregateRequestId
{
    public class AggregateRequest
    {
        public Guid AggregateRequestId { get; set; }

        public AggregateRequest()
        {
            AggregateRequestId = Guid.NewGuid();
        }
    }
}
