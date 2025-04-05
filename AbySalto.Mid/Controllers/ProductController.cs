using AbySalto.Mid.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(int skip = 0, int limit = 30, string sortBy = "title", bool descending = false)
        {
            var products = await _unitOfWork.ProductService.GetProductsAsync(skip, limit, sortBy, descending);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _unitOfWork.ProductService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
