using System;
using System.IO;
using DynamicPdfWithZip.Models;
using IronPdf;
using Microsoft.VisualBasic.CompilerServices;

namespace DynamicPdfWithZip.Services
{
    public class DocumentService: IDocumentService
    {
        public ArchiveFile GetDocument()
        {
            return GetTableOfContents();
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