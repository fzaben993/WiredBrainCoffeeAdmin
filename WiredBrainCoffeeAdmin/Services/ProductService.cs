using Microsoft.EntityFrameworkCore;
using WiredBrainCoffeeAdmin.Data;

namespace WiredBrainCoffeeAdmin.Services
{
    public interface IProductService
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(int id);
        Task<Product> GetById(int id);
        Task<List<Product>> GetAll();
    }

    public class ProductService : IProductService
    {
        private WiredContext _wiredContext;

        public ProductService(WiredContext wiredContext)
        {
            _wiredContext = wiredContext;
        }

        public async Task Add(Product product)
        {
            await _wiredContext.Products.AddAsync(product);
            await _wiredContext.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            _wiredContext.Entry(product).State = EntityState.Modified;
            await _wiredContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _wiredContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            _wiredContext.Products.Remove(product);
            await _wiredContext.SaveChangesAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _wiredContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _wiredContext.Products.ToListAsync();
        }
    }
}
