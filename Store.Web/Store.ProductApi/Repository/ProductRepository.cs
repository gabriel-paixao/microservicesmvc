using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.ProductApi.DbContexts;
using Store.ProductApi.Models;
using Store.ProductApi.Models.Dtos;

namespace Store.ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        //Fields
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        //Constructor
        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //Methods
        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            if (product.ProductId > 0)
            {
                _context.Products.Update(product);
            }
            else
            {
                await _context.Products.AddAsync(product);
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(q => q.ProductId == productId);
                if (product == null) return false;

                _context.Remove(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(q => q.ProductId == productId);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
