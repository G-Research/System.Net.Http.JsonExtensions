using System.Text;
using Newtonsoft.Json;

namespace System.Net.Http.JsonExtensions
{
    public sealed class JsonContent : StringContent
    {
        private const string MediaType = "application/json";

        public JsonContent(object value)
            : base(JsonConvert.SerializeObject(value), Encoding.UTF8, MediaType)
        {
        }
    }
}
