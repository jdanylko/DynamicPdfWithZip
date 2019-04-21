using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using DynamicPdfWithZip.Models;
using IronPdf;

namespace DynamicPdfWithZip.Services
{
    public class DocumentService: IDocumentService
    {
        public ArchiveFile GetDocument()
        {
            return GetTableOfContents();
        }

        public byte[] GetPackage(string path)
        {
            // Start off with our "Table of Contents"
            var fileList = new List<ArchiveFile>
            {
                GetTableOfContents()
            };

            fileList.Add(GetFile($"{path}SwedishChef-InterestingMan.jpg"));
            fileList.Add(GetFile($"{path}HeaviestObjectsInTheUniverse.jpg"));

            return GeneratePackage(fileList);
        }

        private byte[] GeneratePackage(List<ArchiveFile> fileList)
        {
            byte[] result;

            using (var packageStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(packageStream, ZipArchiveMode.Create, true))
                {
                    foreach (var virtualFile in fileList)
                    {
                        //Create a zip entry for each attachment
                        var zipFile = archive.CreateEntry(virtualFile.Name + "." + virtualFile.Extension);
                        using (var sourceFileStream = new MemoryStream(virtualFile.FileBytes))
                        using (var zipEntryStream = zipFile.Open())
                        {
                            sourceFileStream.CopyTo(zipEntryStream);
                        }
                    }
                }
                result = packageStream.ToArray();
            }

            return result;
        }

        private ArchiveFile GetFile(string filePath)
        {
            return new ArchiveFile
            {
                Name = Path.GetFileNameWithoutExtension(filePath),
                Extension = Path.GetExtension(filePath).Replace(".",""),
                FileBytes = File.ReadAllBytes(filePath)
            };
        }

        private ArchiveFile GetTableOfContents()
        {
            // Test parameter.
            var projectId = 5;
            var uri = new Uri("http://localhost:42006/Template/ArchiveHeader/"+projectId);

            var pdf = CreatePdf(uri);
            
            return new ArchiveFile
            {
                Name="Header",
                Extension = "pdf",
                FileBytes = pdf.ToArray()
            };
        }

        private MemoryStream CreatePdf(Uri uri)
        {
            var urlToPdf = new HtmlToPdf();

            // if you want to create a Razor page with custom data,
            // make the call here to call a local HTML page w/ your data
            // in it.

            // var pdf = urlToPdf.RenderUrlAsPdf(uri);

            var pdf = urlToPdf.RenderHtmlAsPdf(@"
                <html>
                <head>
                </head>
                <body>
                    <h1>Table of Contents</h1>
                    <p>This is where you place you custom content!</p>
                </body>
                </html>
            ");

            return pdf.Stream;
        }
    }
}