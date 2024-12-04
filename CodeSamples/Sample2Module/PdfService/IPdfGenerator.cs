using CodeSamples.Sample2Module.Models;

namespace CodeSamples.Sample2Module.PdfService
{
    public interface IPdfGenerator
    {
        FileModel GeneratePdfDocument<TModel, TInputData>(TInputData data)
            where TModel : BasePdfModel
            where TInputData : BasePdfInputData;
    }
}
