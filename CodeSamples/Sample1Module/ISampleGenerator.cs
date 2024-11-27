namespace CodeSamples.Sample1Module
{
    internal interface ISample1Generator
    {
        Sample1ResponseModel GenerateOutput(Sample1RequestModel request, Queue<Sample1HandlerEnum> handlersToUse);
    }
}
