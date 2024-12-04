using CodeSamples.Sample2Module.Models;
using CodeSamples.Sample2Module.PdfService.Content;
using CodeSamples.Sample2Module.PdfService.DataFetchers;
using Microsoft.Extensions.DependencyInjection;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

namespace CodeSamples.Sample2Module.PdfService
{
    public class PdfGenerator : IPdfGenerator
    {
        private readonly IServiceProvider _serviceProvider;

        public PdfGenerator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public FileModel GeneratePdfDocument<TModel, TInputData>(TInputData inputData)
            where TModel : BasePdfModel
            where TInputData : BasePdfInputData
        {
            var pdfDataFetcher = _serviceProvider.GetRequiredService<IPdfDataFetcher<TModel, TInputData>>();
            var content = _serviceProvider.GetRequiredService<BaseContent<TModel, TInputData>>();

            var dataResult = pdfDataFetcher.FetchDataForPdfSummary(inputData);

            var document = new Document { Info = { Author = "Test App" } };

            PdfCommon.DefineBasicStyles(document);

            PdfCommon.DefineBasicLayout(document);

            var fileName = content.DefineContent(document, dataResult, inputData);

            PdfCommon.DefineHeader(document, inputData.AuthorFirstName, inputData.AuthorLastName, inputData.CurrentTimeZone);
            PdfCommon.DefineFooter(document);

            var renderer = new PdfDocumentRenderer { Document = document };

            renderer.RenderDocument();

            using var memoryStream = new MemoryStream();

            renderer.PdfDocument.Save(memoryStream, false);
            var bytes = memoryStream.ToArray();

            return new FileModel { Content = bytes, Name = fileName, Extension = "pdf" };
        }
    }
}
