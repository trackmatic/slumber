﻿using System.Collections.Generic;
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
                result.should_be("/some/api/a/resource/b?p3=c&p4=d");
            };
        }
    }
}
