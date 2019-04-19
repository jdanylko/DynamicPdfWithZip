namespace DynamicPdfWithZip.Models
{
    public class ArchiveFile
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public byte[] FileBytes { get; set; }
    }
}