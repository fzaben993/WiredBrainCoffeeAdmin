using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data;
using WiredBrainCoffeeAdmin.Services;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    public class AddProductModel : PageModel
    {
        private IProductService _productService;
        private IWebHostEnvironment _webHostEnvironment;

        public AddProductModel(
            IProductService productService,
            IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Product NewProduct { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (NewProduct.Upload != null)
            {
                NewProduct.ImageFile = NewProduct.Upload.FileName;

                var file = Path.Combine(_webHostEnvironment.WebRootPath,
                    "images/menu",
                    NewProduct.Upload.FileName);

                await using var fileStream = new FileStream(file, FileMode.Create);

                await NewProduct.Upload.CopyToAsync(fileStream);
            }
            NewProduct.Created = DateTime.Now;

                // save product to database
                await _productService.Add(NewProduct);

                return RedirectToPage("ViewAllProducts");
        }
    }
}
