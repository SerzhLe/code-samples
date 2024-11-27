using CodeSamples.Sample1Module.Handlers;

namespace CodeSamples.Sample1Module
{
    public class Sample1Generator : ISample1Generator
    {
        public Sample1ResponseModel GenerateOutput(Sample1RequestModel request, Queue<Sample1HandlerEnum> handlersToUse)
        {
            var response = new Sample1ResponseModel();
            var queue = handlersToUse ?? new Queue<Sample1HandlerEnum>();
            Sample1HandlerBase handler = null!;

            if (queue.Any())
            {
                handler = Sample1HandlerFactory.GetHandler(queue.Dequeue());
            }

            foreach (var handlerKey in queue)
            {
                handler!.SetHandler(Sample1HandlerFactory.GetHandler(handlerKey));
            }

            handler.Handle(request, response);

            return response;
        }
    }
}
