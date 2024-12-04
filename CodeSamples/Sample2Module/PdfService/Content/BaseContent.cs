using CodeSamples.Sample2Module.Models;
using MigraDoc.DocumentObjectModel;
using System.Globalization;

namespace CodeSamples.Sample2Module.PdfService.Content
{
    public abstract class BaseContent<TModel, TInputData>
        where TModel : BasePdfModel
        where TInputData : BasePdfInputData
    {
        protected string EmptyValue = "-";

        public abstract string DefineContent(Document document, TModel data, TInputData inputData);

        protected string ValueOrDefault<T>(T? value) where T : struct => value.HasValue ? value.Value.ToString() : EmptyValue;

        protected string ValueOrDefault(DateTime? value, string format) => value.HasValue ? value.Value.ToString(format, CultureInfo.InvariantCulture) : EmptyValue;

        protected string ValueOrDefault(string value) => !string.IsNullOrWhiteSpace(value) ? value : EmptyValue;

        protected string ValueOrDefault(string[] values) => values.Any() ? string.Join(", ", values) + "." : EmptyValue;

        protected string ValueOrDefault(string value, string outputWhenNotEmpty) => !string.IsNullOrWhiteSpace(value) ? outputWhenNotEmpty : EmptyValue;
    }
}
