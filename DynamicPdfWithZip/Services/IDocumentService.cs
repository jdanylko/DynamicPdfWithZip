using DynamicPdfWithZip.Models;

namespace DynamicPdfWithZip.Services
{
    public interface IDocumentService
    {
        ArchiveFile GetDocument();
    }
}