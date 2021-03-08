using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace ServerlessCourse.HelloWorld
{
    public class Greeter
    {
        public Task<APIGatewayProxyResponse> Handler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.Log(JsonSerializer.Serialize(request));

            var name = request.QueryStringParameters["name"];

            var body = new {
                message = $"hello {name}"
            };

            var result = new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = JsonSerializer.Serialize(body)
            };

            return Task.FromResult(result); 
        }
    }
}
