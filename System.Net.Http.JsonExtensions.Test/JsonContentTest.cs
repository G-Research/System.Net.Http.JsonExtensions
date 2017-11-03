using System.Threading.Tasks;
using AutoFixture;
using Xunit;

namespace System.Net.Http.JsonExtensions.Test
{
    public class JsonContentTest
    {
        [Fact]
        public async Task JsonContentProducesSensibleOutput()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            var testItems = fixture.CreateMany<TestData>();
            foreach (var testItem in testItems)
            {
                var sut = new JsonContent(testItem);
                var stringData = await sut.ReadAsStringAsync();
                var jsonBoolString = testItem.TestBool ? "true" : "false";
                var expected = $"{{\"TestString\":\"{testItem.TestString}\",\"TestInt\":{testItem.TestInt},\"TestBool\":{jsonBoolString}}}";
                Assert.Equal(expected, stringData);
            }
        }
    }
}
