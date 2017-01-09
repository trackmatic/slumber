using Xunit;

namespace Slumber.Tests.Http
{
    public class ParameterEncoderTests
    {
        [Fact]
        public void ItShouldEncodeQueryParameters()
        {
            var configuration = SlumberConfigurationFactory.Default("http://test.bla");
            var encoder = configuration.UriEncoder.ParameterEncoder;
            var parameter = new QueryParameter
            {
                Name = "key",
                Value = "some value"
            };
            var result = encoder.Encode(parameter);
            Assert.Equal("key=some+value", result);
        }
    }
}
