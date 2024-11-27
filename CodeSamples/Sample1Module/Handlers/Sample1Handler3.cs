namespace CodeSamples.Sample1Module.Handlers
{
    internal class Sample1Handler3 : Sample1HandlerBase
    {
        protected override void ModifyOutput(Sample1RequestModel request, Sample1ResponseModel response)
        {
            // here can be any complex business logic
            if (!string.IsNullOrWhiteSpace(request.Input))
            {
                response.Output = nameof(Sample1Handler3);
            }
        }
    }
}
