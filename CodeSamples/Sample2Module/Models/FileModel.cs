namespace CodeSamples.Sample2Module.Models
{
    public class FileModel
    {
        public FileModel()
        {
        }

        public FileModel(string name, byte[] content)
        {
            Name = name;
            Content = content;
        }

        public string Name { get; set; } = "fileName";

        public byte[] Content { get; set; } = Array.Empty<byte>();

        public string Extension { get; set; } = "pdf";
    }
}
