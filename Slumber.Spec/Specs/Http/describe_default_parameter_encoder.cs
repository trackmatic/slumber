using NSpec;
using Slumber.Http;

namespace Slumber.Spec.Specs.Http
{
    public class describe_default_parameter_encoder : nspec
    {
        public void describe_encode()
        {
            it["should encode the query parameter with HttpUtility.UrlEncode functionality"] = () =>
            {
                var encoder = new HttpParameterEncoder();
                var parameter = new QueryParameter
                {
                    Name = "key",
                    Value = "some value"
                };
                var result = encoder.Encode(parameter);
                result.should_be("key=some+value");
            };
        }
    }
}
