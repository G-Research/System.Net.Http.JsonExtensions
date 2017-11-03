using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http.JsonExtensions.Test
{
    internal class MockHttpClientHandler : HttpClientHandler
    {
        private readonly List<CapturedRequest> _capturedRequests;

        public MockHttpClientHandler(string returnedMessageContent)
        {
             _capturedRequests = new List<CapturedRequest>();
            MessageResponse = new HttpResponseMessage()
            {
                Content = new StringContent(returnedMessageContent),
            };
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _capturedRequests.Add(new CapturedRequest(request, cancellationToken));
            return Task.FromResult(MessageResponse);
        }

        public IReadOnlyCollection<CapturedRequest> CapturedRequests => _capturedRequests;

        public HttpResponseMessage MessageResponse { get; }

        public class CapturedRequest
        {
            public CapturedRequest(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Request = request;
                CancellationToken = cancellationToken;
            }

            public HttpRequestMessage Request { get; }
            public CancellationToken CancellationToken { get; }
        }
    }
}
