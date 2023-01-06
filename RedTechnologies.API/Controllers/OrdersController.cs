using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedTechnologies.Application.Interfaces;
using RedTechnologies.Application.Model;
using RedTechnologies.Domain.Entities;
using RedTechnologies.Domain.Enumerators;
using System.Net;

namespace RedTechnologies.API.Controllers
{
    /// <summary>
    /// Orders Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderAppService _orderApp;
        private IMapper Mapper
        {
            get;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="orderApp">The order application.</param>
        /// <param name="mapper">The mapper.</param>
        public OrdersController(IOrderAppService orderApp, IMapper mapper)
        {
            _orderApp = orderApp;
            this.Mapper = mapper;
        }


        /// <summary>
        /// Get all Orders
        /// </summary>
        [HttpGet]
        [Authorize()]
        public virtual IActionResult GetOrders()
        {
            try
            {
                return Ok(Mapper.Map<IEnumerable<OrderModel>>(_orderApp.GetAll()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Update a Order
        /// </summary>
        [HttpPut]
        [Authorize()]
        public virtual IActionResult UpdateOrder([FromBody] OrderModel order)
        {
            try
            {
                if (order != null)
                {
                    var orderDto = Mapper.Map<Order>(order);
                    _orderApp.Update(orderDto);
                    return Ok();
                }
                else return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Create a Order
        /// </summary>
        [HttpPost]
        [Authorize()]
        public virtual IActionResult CreateOrder([FromBody] OrderModel order)
        {
            try
            {
                if (order != null)
                {
                    var orderDto = Mapper.Map<Order>(order);                 
                    return Ok(_orderApp.Add(orderDto));
                }
                else return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get a list of Orders By Type
        /// </summary>
        /// <remarks>
        /// Valid values: Standard, SaleOrder, PurchaseOrder, TransferOrder, ReturnOrder
        /// </remarks>
        [HttpGet("ByType")]
        [Authorize()]
        public virtual IActionResult GetOrderByType(string orderType)
        {
            try
            {              
                if (!string.IsNullOrEmpty(orderType))
                {
                    List<OrderModel> x = Mapper.Map<IEnumerable<OrderModel>>(_orderApp.GetByFilter(c => c.Type == _orderApp.CheckEnum(orderType))).ToList();
                    return Ok(Mapper.Map<IEnumerable<OrderModel>>(_orderApp.GetByFilter(c => c.Type == _orderApp.CheckEnum(orderType))));
                }
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException?.Message);
            }
        }

        /// <summary>
        /// Delete a Order
        /// </summary>
        [HttpPost("Delete")]
        [Authorize()]
        public virtual IActionResult DeleteOrder(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var order = _orderApp.GetById(new Guid(id));
                    if (order != null)
                    {
                        _orderApp.Remove(order);
                        return Ok();
                    }
                    else return BadRequest();

                }
                else return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get a order by CustomerName
        /// </summary>
        [HttpGet("GetByCustomerName")]
        [Authorize()]
        public virtual IActionResult GetByCustomer(string customerName)
        {
            try
            {
                if (!string.IsNullOrEmpty(customerName))
                {
                   return Ok(Mapper.Map<IEnumerable<OrderModel>>(_orderApp.GetByFilter(c => c.CustomerName.StartsWith(customerName) || c.CustomerName.EndsWith(customerName))));                   
                }
                else return BadRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}