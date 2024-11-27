namespace CodeSamples.Sample1Module.Handlers
{
    internal class Sample1Handler1 : Sample1HandlerBase
    {
        protected override void ModifyOutput(Sample1RequestModel request, Sample1ResponseModel response)
        {
            // here can be any complex business logic
            // Sample1ResponseModel can contain more properties to store output data from all handlers
            if (!string.IsNullOrWhiteSpace(request.Input))
            {
                response.Output = nameof(Sample1Handler1);
            }
        }
    }
}
