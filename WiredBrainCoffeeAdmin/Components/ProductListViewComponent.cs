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

    public async Task<IViewComponentResult> InvokeAsync(int count)
    {
        var products = await _productService.GetAll();

        if (count > 0)
        {
            return View(products.Take(count).ToList());

        }

        return View(products);
    }
}
