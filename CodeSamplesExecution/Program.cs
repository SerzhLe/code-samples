using CodeSamples.Sample1Module;

var sample1Generator = new Sample1Generator();
var sample1Request = new Sample1RequestModel { Input = "Test" };
var handlersToUse = new Queue<Sample1HandlerEnum>(new Sample1HandlerEnum[]
{
    Sample1HandlerEnum.UseHandler2,
    Sample1HandlerEnum.UseHandler1
});

var result = sample1Generator.GenerateOutput(sample1Request, handlersToUse);

Console.ReadKey();
