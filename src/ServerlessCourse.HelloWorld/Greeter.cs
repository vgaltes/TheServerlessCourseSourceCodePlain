using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Text.Json;
using Voxel.MiddyNet;
using Voxel.MiddyNet.SSMMiddleware;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace ServerlessCourse.HelloWorld
{
    public class Greeter : MiddyNet<APIGatewayProxyRequest, APIGatewayProxyResponse>
    {
        public Greeter()
        {
            Use(new SSMMiddleware<APIGatewayProxyRequest, APIGatewayProxyResponse>( new SSMOptions{
                CacheExpiryInMillis = 60000,
                ParametersToGet = new System.Collections.Generic.List<SSMParameterToGet>{
                    new SSMParameterToGet("TableName", "/gettogethers/dev/secureTableName")
                }
            }));
        }
        protected override Task<APIGatewayProxyResponse> Handle(APIGatewayProxyRequest request, MiddyNetContext context)
        {
            var name = request.QueryStringParameters["name"];

            var body = new {
                message = $"hello {name}"
            };

            context.Logger.EnrichWith(new LogProperty("propiedadExtra", name));
            context.Logger.EnrichWith(new LogProperty("tableName", 
                context.AdditionalContext["TableName"]));
            context.Logger.Log(LogLevel.Info, "estamos dentro");

            var result = new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = JsonSerializer.Serialize(body)
            };

            return Task.FromResult(result); 
        }
    }
}
