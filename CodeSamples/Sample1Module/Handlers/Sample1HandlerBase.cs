namespace CodeSamples.Sample1Module.Handlers
{
    internal abstract class Sample1HandlerBase
    {
        private Sample1HandlerBase? _handler;

        public Sample1HandlerBase SetHandler(Sample1HandlerBase handler)
        {
            _handler = handler;
            return _handler;
        }

        public void Handle(Sample1RequestModel request, Sample1ResponseModel response)
        {
            ModifyOutput(request, response);

            _handler?.Handle(request, response);
        }

        protected abstract void ModifyOutput(Sample1RequestModel request, Sample1ResponseModel response);
    }
}
