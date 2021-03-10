using Amazon.Lambda.APIGatewayEvents;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using System.Threading.Tasks;
using System.Text.Json;
using Amazon.Lambda.Core;
using Amazon.XRay.Recorder.Core;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace ServerlessCourse.HelloWorld
{
    public class Sender
    {
        public Sender()
        {
            AWSSDKHandler.RegisterXRayForAllServices();
        }

        public async Task<APIGatewayProxyResponse> Handler(APIGatewayProxyRequest lambdaEvent, ILambdaContext context)
        {
            var recorder = new AWSXRayRecorder();
            recorder.BeginSubsegment("UpperCase");
            recorder.BeginSubsegment("Recuperando el nombre");
            var name = lambdaEvent.PathParameters["name"];
            recorder.EndSubsegment();

            recorder.BeginSubsegment("enviando el mensaje");
            var snsClient = new AmazonSimpleNotificationServiceClient();
            var request = new PublishRequest
            {
                Message = $"hello from the other side, {name}",
                TopicArn = Environment.GetEnvironmentVariable("topicArn")
            };

            await snsClient.PublishAsync(request);
            recorder.EndSubsegment();
            recorder.EndSubsegment();

            var body = new {
                message = "job done"
            };
            var result = new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = JsonSerializer.Serialize(body)
            };
            return result;
        }
    }
}