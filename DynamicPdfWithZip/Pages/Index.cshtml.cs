using System;
using System.IO;
using DynamicPdfWithZip.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DynamicPdfWithZip.Pages
{
    public class IndexModel : PageModel
    {
        private IDocumentService _service;
        private readonly IHostingEnvironment _env;

        public IndexModel(IDocumentService service, IHostingEnvironment env)
        {
            _service = service;
            _env = env;
        }

        public void OnGet() { }

        public FileContentResult OnPostDownloadPdf()
        {
            var document = _service.GetDocument();

            var file = document.Name + "."+document.Extension;

            return File(document.FileBytes, "application/pdf", file);
        }

        public FileContentResult OnPostDownloadZip()
        {
            var path = Path.Combine(_env.WebRootPath, @"images\");
            var package = _service.GetPackage(path);
            
            var file = DateTime.UtcNow.ToString("yy-MMM-dd")+"-Package.zip";

            return File(package, "application/zip", file);
        }

    }
}
