using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;

namespace ServerlessCourse.HelloWorld
{
    public class Receiver
    {
        public Task<string> Handler(SNSEvent lambdaEvent, ILambdaContext context)
        {
            context.Logger.Log($"Mensaje recibido ${lambdaEvent.Records[0].Sns.Message}");
            return Task.FromResult("all done");
        }
    }
}