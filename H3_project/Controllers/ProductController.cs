using H3_project.Models;

namespace H3_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository productRepository;

        public ProductController(IProductRepository temp)
        {
            productRepository = temp;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProduct()
        {
            try
            {
                var product = await productRepository.GetProduct();

                if (product == null)
                {
                    return Problem("Nothing was returned from category service, this is unexpected");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet("{productId}")]
        public async Task<ActionResult> GetProductById(int productId)
        {
            try
            {
                var product = await productRepository.GetProductById(productId);

                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //Update Method
        [HttpPut("{productId}")]

        public async Task<ActionResult> PutProduct(Product product, int productId)
        {
            try
            {
                var productResult = await productRepository.UpdateProduct(productId, product);

                if (product == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(product);
        }

        // Create Method
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                var createProduct = await productRepository.CreateProduct(product);

                if (createProduct == null)
                {
                    return StatusCode(500, "Product was not created. Something failed...");
                }
                return CreatedAtAction("PostProduct", new { productId = createProduct.ProductID }, createProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the Product {ex.Message}");
            }
        }

        // Delete Method
        [HttpDelete("{productId}")]

        public async Task<ActionResult> DeleteProduct(int productId)
        {
            try
            {
                var product = await productRepository.DeleteProduct(productId);

                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
