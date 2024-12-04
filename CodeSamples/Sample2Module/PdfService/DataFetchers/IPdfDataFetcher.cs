using CodeSamples.Sample2Module.Models;

namespace CodeSamples.Sample2Module.PdfService.DataFetchers
{
    public interface IPdfDataFetcher<TModel, TInputData>
        where TModel : BasePdfModel
        where TInputData : BasePdfInputData
    {
        TModel FetchDataForPdfSummary(TInputData data);
    }
}
