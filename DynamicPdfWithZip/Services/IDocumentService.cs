using System.IO.Compression;
using DynamicPdfWithZip.Models;

namespace DynamicPdfWithZip.Services
{
    public interface IDocumentService
    {
        ArchiveFile GetDocument();
        byte[] GetPackage(string path);
    }
}