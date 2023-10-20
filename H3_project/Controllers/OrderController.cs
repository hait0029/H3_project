using H3_project.Models;

namespace H3_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository orderRepository;

        public OrderController(IOrderRepository temp)
        {
            orderRepository = temp;
        }

        //(Read)
        //Get: api/Order
        [HttpGet]
        public async Task<ActionResult> GetAllOrder()
        {
            try
            {
                var order = await orderRepository.GetOrder();

                if (order == null)
                {
                    return Problem("Nothing was returned from category service, this is unexpected");
                }
                
                return Ok(order);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //(Read)
        //Get: api/Order/Id
        [HttpGet("{orderId}")]
        public async Task<ActionResult> GetOrderById(int orderId)
        {
            try
            {
                var order = await orderRepository.GetOrderById(orderId);

                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //(Update)
        //Put: api/order/Id
        [HttpPut("{orderId}")]
        public async Task<ActionResult> PutOrder(Order order, int orderId)
        {
            try
            {
                var orderResult = await orderRepository.UpdateOrder(orderId, order);

                if (order == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return Ok(order);
        }

        //(Create)
        //Post: api/order
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            try
            {
                var createdOrder = await orderRepository.CreateOrder(order);

                if (createdOrder == null)
                {
                    return StatusCode(500, "User was not created. Something failed...");
                }
                return CreatedAtAction("PostOrder", new { orderId = createdOrder.OrderID }, createdOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured while creating the Order: {ex.Message}");
            }
        }

        //(Delete)
        //Delete: api/Order/Id
        [HttpDelete("{orderId}")]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            try
            {
                var order = await orderRepository.DeleteOrder(orderId);

                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
