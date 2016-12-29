using System.Collections.Generic;
using Moq;
using Slumber.Http;
using Xunit;

namespace Slumber.Tests
{
    public class UriEncoderTests
    {
        [Fact]
        public void ItShouldGenerateValidUris()
        {

            var restQueryParameterEncoder = new HttpParameterEncoder();
            var request = new Mock<IRequest>();
            request.Setup(x => x.Query).Returns(new List<QueryParameter>
                {
                    new QueryParameter
                    {
                        Name = "p1",
                        Value = "a"
                    },
                    new QueryParameter
                    {
                        Name = "p2",
                        Value = "b"
                    },
                    new QueryParameter
                    {
                        Name = "p3",
                        Value = "c"
                    },
                    new QueryParameter
                    {
                        Name = "p4",
                        Value = "d"
                    }
                });
            request.Setup(x => x.Path).Returns("/some/api/{p1}/resource/{p2}");
            var encoder = new DefaultUriEncoder(restQueryParameterEncoder);
            var result = encoder.Encode(request.Object);
            Assert.Equal("/some/api/a/resource/b?p3=c&p4=d", result);
        }
    }
}
