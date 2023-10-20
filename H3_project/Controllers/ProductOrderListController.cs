namespace H3_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOrderListController : ControllerBase
    {
        private IProductOrderListRepository productOrderListRepository;

        public ProductOrderListController(IProductOrderListRepository temp)
        {
            productOrderListRepository = temp;
        }

        //(Read)
        //Get: api/ProductOrderList
        [HttpGet]
        public async Task<ActionResult> GetAllProductOrderList()
        {
            try
            {
                var productOrderList = await productOrderListRepository.GetProductOrderList();

                if (productOrderList == null)
                {
                    return Problem("Nothing was returned from productOrderList, this is unexpected");
                }

                return Ok(productOrderList);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //(Read)
        //Get: api/ProductOrderList/Id
        [HttpGet("{productOrderListId}")]
        public async Task<ActionResult> GetproductOrderListById(int productOrderListId)
        {
            try
            {
                var productOrderList = await productOrderListRepository.GetProductOrderListById(productOrderListId);

                if (productOrderList == null)
                {
                    return NotFound();
                }
                return Ok(productOrderList);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //(Update)
        //Put: api/ProductOrderList/Id
        [HttpPut("{productOrderListId}")]
        public async Task<ActionResult> PutProductOrderList(ProductOrderList productOrderList, int productOrderListId)
        {
            try
            {
                var productOrderListResult = await productOrderListRepository.UpdateProductOrderList(productOrderListId, productOrderList);

                if (productOrderList == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(productOrderList);
        }

        //(Create)
        //Post: api/ProductOrderList
        [HttpPost]
        public async Task<ActionResult<ProductOrderList>> PostProductOrderList(ProductOrderList productOrderList)
        {
            try
            {
                var createdProductOrderList = await productOrderListRepository.CreateProductOrderList(productOrderList);

                if (createdProductOrderList == null)
                {
                    return StatusCode(500, "User was not created. Something failed...");
                }
                return CreatedAtAction("PostProductOrderList", new { productOrderListId = createdProductOrderList.ProductOrderListID }, createdProductOrderList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the Order: {ex.Message}");
            }
        }
        //(Delete)
        //Delete: api/productOrderList/Id
        [HttpDelete("{productOrderListId}")]
        public async Task<ActionResult> DeleteProductOrderList(int productOrderListId)
        {
            try
            {
                var productOrderList = await productOrderListRepository.DeleteProductOrderList(productOrderListId);

                if (productOrderList == null)
                {
                    return NotFound();
                }
                return Ok(productOrderList);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
