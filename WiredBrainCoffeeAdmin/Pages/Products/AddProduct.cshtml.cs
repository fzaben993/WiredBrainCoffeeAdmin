using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    public class AddProductModel : PageModel
    {
        private WiredContext _wiredContext;
        private IWebHostEnvironment _webHostEnvironment;

        public AddProductModel(WiredContext context, IWebHostEnvironment webHostEnvironment)
        {
            _wiredContext = context;
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

                var file = Path.Combine(_webHostEnvironment.WebRootPath, "images/menu", NewProduct.Upload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await NewProduct.Upload.CopyToAsync(fileStream);
                }
            }
            NewProduct.Created = DateTime.Now;

                // save product to database
                await _wiredContext.Products.AddAsync(NewProduct);
                var changes = await _wiredContext.SaveChangesAsync();

                return RedirectToPage("ViewAllProducts");
        }
    }
}
