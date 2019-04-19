using System.Linq;
using DynamicPdfWithZip.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DynamicPdfWithZip.Pages
{
    public class IndexModel : PageModel
    {
        private IDocumentService _service;

        public IndexModel(IDocumentService service)
        {
            _service = service;
        }

        public void OnGet()
        {

        }

        public FileContentResult OnPostDownload()
        {
            var document = _service.GetDocument();

            var file = document.Name + "."+document.Extension;

            return File(document.FileBytes, "application/pdf", file);
        }

    }
}
