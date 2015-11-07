using System.Collections.Generic;
using Moq;
using NSpec;
using Slumber.Http;

namespace Slumber.Spec.Specs
{
    public class describe_default_uri_encoder : nspec
    {
        public void describe_encode()
        {
            it["should build up a valid url"] = () =>
            {
                var restQueryParameterEncoder = new HttpParameterEncoder();
                var request = new Mock<IRestRequest>();
                request.Setup(x => x.Query).Returns(new List<RestQueryParameter>
                {
                    new RestQueryParameter
                    {
                        Name = "p1",
                        Value = "a"
                    },
                    new RestQueryParameter
                    {
                        Name = "p2",
                        Value = "b"
                    },
                    new RestQueryParameter
                    {
                        Name = "p3",
                        Value = "c"
                    },
                    new RestQueryParameter
                    {
                        Name = "p4",
                        Value = "d"
                    }
                });
                request.Setup(x => x.Path).Returns("/some/api/{p1}/resource/{p2}");
                var encoder = new DefaultUriEncoder(restQueryParameterEncoder);
                var result = encoder.Encode(request.Object);
                result.should_be("/some/api/a/resource/b?p3=c&p4=d");
            };
        }
    }
}
