using Dto;
using Microsoft.AspNetCore.Mvc;
using Services;
namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService _s;
        public OrderController(IOrderService i)
        {
            _s = i;
        }



        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<DtoOrderIdUserIdDateSumOrderItems>>> GetByUser(int userId)
        {
            var order = await _s.GetOrdersUser(userId);
            if (order != null)
            {
                return Ok(order);
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DtoOrderIdUserIdDateSumOrderItems>> Get(int id)
        {
            DtoOrderIdUserIdDateSumOrderItems order = await _s.GetOrderById(id);
            if (order != null)
            {
                return Ok(order);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<DtoOrderIdUserIdDateSumOrderItems>> Post([FromBody] DtoOrderIdUserIdDateSumOrderItems order)
        {

            DtoOrderIdUserIdDateSumOrderItems res = await _s.AddNewOrder(order);
            if (res != null)
            {
                return CreatedAtAction(nameof(Get), new { id = res.OrderId }, res);
            }
            else
                return BadRequest();
        }

    }
}
