using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data;
using WiredBrainCoffeeAdmin.Services;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    public class ViewAllProductsModel : PageModel
    {
        private readonly IProductService _productService;

        public List<Product> Products { get; set; }

        public ViewAllProductsModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task OnGet()
        {
            Products = await _productService.GetAll();
        }
    }
}
