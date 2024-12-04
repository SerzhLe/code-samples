using CodeSamples.Sample2Module.Models;
using MigraDoc.DocumentObjectModel;

namespace CodeSamples.Sample2Module.PdfService.Content
{
    public class SampleContent : BaseContent<SamplePdfModel, SamplePdfInputData>
    {
        public override string DefineContent(Document document, SamplePdfModel data, SamplePdfInputData inputData)
        {
            document.Info.Title = $"Sample - {data.Data}";
            document.Info.Subject = $"Sample pdf content";

            var section = document.LastSection;

            section.AddParagraph($"Simple test content - {data.Data}");

            return $"Sample_{inputData.Number}";
        }
    }
}
