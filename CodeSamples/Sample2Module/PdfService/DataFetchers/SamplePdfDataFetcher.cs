using CodeSamples.Sample2Module.Models;

namespace CodeSamples.Sample2Module.PdfService.DataFetchers
{
    public class SamplePdfDataFetcher : IPdfDataFetcher<SamplePdfModel, SamplePdfInputData>
    {
        private Dictionary<int, string> _data = new Dictionary<int, string>
        {
            { 1, "Data number one" },
            { 2, "Data number two" },
            { 3, "Data number three" }
        };

        public SamplePdfModel FetchDataForPdfSummary(SamplePdfInputData data)
        {
            // here must be a call to db to fetch all necessary data for pdf rendering
            var result = _data.TryGetValue(data.Number, out var value);

            return result ? new SamplePdfModel { Id = Guid.NewGuid(), Data = value } : new SamplePdfModel();
        }
    }
}
