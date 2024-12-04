namespace CodeSamples.Sample2Module.Models
{
    public abstract class BasePdfInputData
    {
        public Guid EntityId { get; set; }

        public string CurrentTimeZone { get; set; } = "Etc/UTC";

        public string AuthorFirstName { get; set; } = string.Empty;

        public string AuthorLastName { get; set; } = string.Empty;
    }
}
