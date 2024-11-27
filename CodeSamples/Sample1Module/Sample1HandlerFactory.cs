using CodeSamples.Sample1Module.Handlers;

namespace CodeSamples.Sample1Module
{
    internal static class Sample1HandlerFactory
    {
        private static readonly Dictionary<Sample1HandlerEnum, Type> _handlerMapping = new Dictionary<Sample1HandlerEnum, Type>
        {
            { Sample1HandlerEnum.UseHandler1, typeof(Sample1Handler1) },
            { Sample1HandlerEnum.UseHandler2, typeof(Sample1Handler2) },
            { Sample1HandlerEnum.UseHandler3, typeof(Sample1Handler3) }
        };

        // in real-world app handlers can be instantiated using DI
        public static Sample1HandlerBase GetHandler(Sample1HandlerEnum key)
        {
            if (!_handlerMapping.TryGetValue(key, out var handlerType))
            {
                throw new InvalidOperationException("Mapping does not have appropriate handler for this key.");
            }

            return (Activator.CreateInstance(handlerType) as Sample1HandlerBase)!;
        }
    }
}
