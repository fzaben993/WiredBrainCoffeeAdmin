using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data;
using WiredBrainCoffeeAdmin.Services;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    public class EditProductModel : PageModel
    {
        private IProductService _productService;
        private IWebHostEnvironment _webHostEnvironment;

        public EditProductModel(
            IProductService productService,
            IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        [FromRoute]
        public int Id { get; set; }


        [BindProperty]
        public Product EditProduct { get; set; }

        public async void OnGet()
        {
            EditProduct =  await _productService.GetById(Id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (EditProduct.Upload != null)
            {
                EditProduct.ImageFile = EditProduct.Upload.FileName;

                var file = Path.Combine(_webHostEnvironment.WebRootPath,
                    "images/menu",
                    EditProduct.Upload.FileName);

                await using var fileStream = new FileStream(file, FileMode.Create);

                await EditProduct.Upload.CopyToAsync(fileStream);
            }

            EditProduct.Id= Id;
            // save product to database
            await _productService.Update(EditProduct);

            return RedirectToPage("ViewAllProducts");
        }
    }
}