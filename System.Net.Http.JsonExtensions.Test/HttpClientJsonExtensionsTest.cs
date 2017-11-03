using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Xunit;

namespace System.Net.Http.JsonExtensions.Test
{
    public sealed class HttpClientJsonExtensionsTest : IDisposable
    {
        private readonly MockHttpClientHandler _handler;
        private readonly HttpClient _client;
        private readonly CancellationTokenSource _cts;
        private readonly TestData _testData;
        private readonly string _url;
        private readonly string _data;

        public HttpClientJsonExtensionsTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            _testData = fixture.Create<TestData>();
            var jsonBoolString = _testData.TestBool ? "true" : "false";
            _handler = new MockHttpClientHandler($"{{\"TestString\":\"{_testData.TestString}\",\"TestInt\":{_testData.TestInt},\"TestBool\":{jsonBoolString}}}");
            _client = new HttpClient(_handler);
            _cts = new CancellationTokenSource();
            var urlBody = fixture.Create<string>();
            _url = $"http://{urlBody}/";
            _data = fixture.Create<string>();
        }

        [Fact]
        public async Task PostAsJsonAsyncWithRequestStringMakesSensibleRequest()
        {
            var message = await _client.PostAsJsonAsync(_url, _data);
            await VerifySend(message);
        }

        [Fact]
        public async Task PostAsJsonAsyncWithRequestStringAndCancellationTokenMakesSensibleRequest()
        {
            var message = await _client.PostAsJsonAsync(_url, _data, _cts.Token);
            await VerifySend(message);
        }

        [Fact]
        public async Task PostAsJsonAsyncWithRequestUriMakesSensibleRequest()
        {
            var message = await _client.PostAsJsonAsync(new Uri(_url), _data);
            await VerifySend(message);
        }

        [Fact]
        public async Task PostAsJsonAsyncWithRequestUriAndCancellationTokenMakesSensibleRequest()
        {
            var message = await _client.PostAsJsonAsync(new Uri(_url), _data, _cts.Token);
            await VerifySend(message);
        }

        [Fact]
        public async Task PutAsJsonAsyncWithRequestStringMakesSensibleRequest()
        {
            var message = await _client.PutAsJsonAsync(_url, _data);
            await VerifySend(message);
        }

        [Fact]
        public async Task PutAsJsonAsyncWithRequestStringAndCancellationTokenMakesSensibleRequest()
        {
            var message = await _client.PutAsJsonAsync(_url, _data, _cts.Token);
            await VerifySend(message);
        }

        [Fact]
        public async Task PutAsJsonAsyncWithRequestUriMakesSensibleRequest()
        {
            var message = await _client.PutAsJsonAsync(new Uri(_url), _data);
            await VerifySend(message);
        }

        [Fact]
        public async Task PutAsJsonAsyncWithRequestUriAndCancellationTokenMakesSensibleRequest()
        {
            var message = await _client.PutAsJsonAsync(new Uri(_url), _data, _cts.Token);
            await VerifySend(message);
        }

        [Fact]
        public async Task GetAsJsonAsyncWithRequestStringReturnsCorrectObject()
        {
            var result = await _client.GetAsJsonAsync<TestData>(_url);
            Assert.Equal(_testData, result);
        }

        [Fact]
        public async Task GetAsJsonAsyncWithRequestStringAndCancellationTokenReturnsCorrectObject()
        {
            var result = await _client.GetAsJsonAsync<TestData>(_url, _cts.Token);
            Assert.Equal(_testData, result);
        }

        [Fact]
        public async Task GetAsJsonAsyncWithRequestUriReturnsCorrectObject()
        {
            var result = await _client.GetAsJsonAsync<TestData>(_url);
            Assert.Equal(_testData, result);
        }

        [Fact]
        public async Task GetAsJsonAsyncWithRequestUriAndCancellationTokenReturnsCorrectObject()
        {
            var result = await _client.GetAsJsonAsync<TestData>(_url, _cts.Token);
            Assert.Equal(_testData, result);
        }

        public void Dispose()
        {
            _client.Dispose();
            _handler.Dispose();
            _cts.Dispose();
        }

        private async Task VerifySend(HttpResponseMessage message)
        {
            Assert.Equal(_handler.MessageResponse, message);
            var capturedRequest = Assert.Single(_handler.CapturedRequests);
            Assert.Equal(_url, capturedRequest.Request.RequestUri.AbsoluteUri);
            //We could be checking the cancellation token here, but there's a runtime bug:
            //https://connect.microsoft.com/VisualStudio/feedback/details/1214051
            var content = capturedRequest.Request.Content;
            Assert.IsType<JsonContent>(content);
            var contentString = await content.ReadAsStringAsync();
            Assert.Equal(contentString, $"\"{_data}\"");
        }
    }
}
