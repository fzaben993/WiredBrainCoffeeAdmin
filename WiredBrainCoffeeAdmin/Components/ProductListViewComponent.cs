using Microsoft.AspNetCore.Mvc;
using WiredBrainCoffeeAdmin.Services;

namespace WiredBrainCoffeeAdmin.Components;
public class ProductListViewComponent : ViewComponent
{
    private readonly IProductService _productService;

    public ProductListViewComponent(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var products = await _productService.GetAll();

        return View(products);
    }
}
