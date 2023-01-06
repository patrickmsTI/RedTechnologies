using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RedTechnologies.API.Controllers;
using RedTechnologies.Application.Interfaces;
using RedTechnologies.Application.Model;
using RedTechnologies.Application.Services;
using RedTechnologies.Domain.Entities;
using RedTechnologies.Domain.Interfaces.Repositories;
using RedTechnologies.Domain.Interfaces.Services;
using System.Linq.Expressions;

namespace RedTechnologies.Test.StepDefinitions
{
    [Binding]
    public class OrderControllerSteps
    {
        private OrdersController _controller;
        private IActionResult _result;
        private readonly IOrderAppService _orderAppService;
        private readonly Mock<IOrderService> _orderService;
        private readonly Mock<IOrderRepository> _repository;

        public OrderControllerSteps()
        {
            _repository = new Mock<IOrderRepository>();
            _orderService = new Mock<IOrderService>();
            _orderAppService = new OrderAppService(_orderService.Object);

        }

        #region Create new Order
        [BeforeScenario("addneworder")]
        public void SetUpAddNewOrder()
        {
            #region Mock Mapper
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(c => c.Map<Order>(It.IsAny<OrderModel>())).Returns(new Order
            {
                CustomerName = "Test",
                Type = Domain.Enumerators.OrderType.Standard,
                CreatedByUsername = "admin"
            });
            #endregion

            #region Mock OrderRepository
            var mockIOrderRepository = new Mock<IOrderRepository>();
            mockIOrderRepository.Setup(c => c.GetNextId()).Returns(new Guid());
            mockIOrderRepository.Setup(c => c.Add(It.IsAny<Order>())).Returns(
                new Order
                {
                    Id = new Guid("655b293d-e02b-4c2c-beeb-2cf4c404aef4"),
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now,
                    CustomerName = "Test",
                    Type = Domain.Enumerators.OrderType.Standard
                });
            #endregion

            _controller = new OrdersController(_orderAppService, mockMapper.Object);

        }

        [When("asked to create a new order")]
        public void WhenAskedToCreateNewOrder()
        {
            var _payload = new OrderModel
            {
                CustomerName = "Test",
                Type = "Standard",
                CreatedByUsername = "admin"
            };

            _result = _controller.CreateOrder(_payload);
        }        
        #endregion

        #region Update a Order
        [BeforeScenario("updateaorder")]
        public void SetUpUpdateOrder()
        {
            #region Mock Mapper
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(c => c.Map<Order>(It.IsAny<OrderModel>())).Returns(new Order
            {
                Id = new Guid("9b87a766-2334-4d20-8d97-8e4820fe3ffd"),
                CustomerName = "Test",
                Type = Domain.Enumerators.OrderType.Standard,
                CreatedByUsername = "admin",
                CreatedDate = DateTime.Now
            });
            #endregion

            #region Mock OrderRepository
            var mockIOrderRepository = new Mock<IOrderRepository>();
            mockIOrderRepository.Setup(c => c.Update(It.IsAny<Order>()));
            #endregion

            _controller = new OrdersController(_orderAppService, mockMapper.Object);

        }

        [When("asked to update a order")]
        public void WhenAskedToUpdateOrder()
        {
            var _payload = new OrderModel
            {
                Id = "655b293d-e02b-4c2c-beeb-2cf4c404aef4",
                CustomerName = "Test",
                Type = "Standard",
                CreatedDate = DateTime.Now.ToString(),
                CreatedByUsername = "admin"
            };

            _result = _controller.UpdateOrder(_payload);
        }


        #endregion

        #region Get All Orders
        [BeforeScenario("getallorders")]
        public void SetUpGetAllOrders()
        {
            #region Mock Mapper
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(c => c.Map<IEnumerable<OrderModel>>(It.IsAny<IEnumerable<Order>>())).Returns(
                 new List<OrderModel>
                {
                    new OrderModel{
                    Id = "655b293d-e02b-4c2c-beeb-2cf4c404aef4",
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now.ToString(),
                    CustomerName = "Test",
                    Type = Domain.Enumerators.OrderType.Standard.ToString()
                    },
                    new OrderModel{
                    Id = "1972610b-dfb3-4551-bfcc-52da1d9fc254",
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now.ToString(),
                    CustomerName = "Frank",
                    Type = Domain.Enumerators.OrderType.Standard.ToString()
                    },new OrderModel{
                    Id = "9b87a766-2334-4d20-8d97-8e4820fe3ffd",
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now.ToString(),
                    CustomerName = "John",
                    Type = Domain.Enumerators.OrderType.Standard.ToString()
                    },
                }
                );
            #endregion

            #region Mock OrderRepository
            var mockIOrderRepository = new Mock<IOrderRepository>();
            mockIOrderRepository.Setup(c => c.GetAll()).Returns(
                new List<Order>
                {
                    new Order{
                    Id = new Guid("655b293d-e02b-4c2c-beeb-2cf4c404aef4"),
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now,
                    CustomerName = "Test",
                    Type = Domain.Enumerators.OrderType.Standard
                    },
                    new Order{
                    Id = new Guid("1972610b-dfb3-4551-bfcc-52da1d9fc254"),
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now,
                    CustomerName = "Frank",
                    Type = Domain.Enumerators.OrderType.SaleOrder
                    },new Order{
                    Id = new Guid("9b87a766-2334-4d20-8d97-8e4820fe3ffd"),
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now,
                    CustomerName = "John",
                    Type = Domain.Enumerators.OrderType.TransferOrder
                    },
                });
            #endregion

            _controller = new OrdersController(_orderAppService, mockMapper.Object);

        }

        [When("asked to get all orders")]
        public void WhenAskedToGetAllOrders()
        {
            _result = _controller.GetOrders();
        }

        [Then("should return success a order list")]
        public void ThenShouldReturnSuccessAOrderList()
        {
            var okResult = _result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult?.StatusCode.Should().Be(200);
            List<OrderModel> orderList = (List<OrderModel>)okResult.Value;
            orderList.Any().Should().BeTrue();

        }
        #endregion

        #region Delete a Order
        [BeforeScenario("deleteaorder")]
        public void SetUpDeleteAOrder()
        {
            #region Mock Mapper
            var mockMapper = new Mock<IMapper>();
            #endregion

            #region Mock OrderRepository
            var mockIOrderRepository = new Mock<IOrderRepository>();
            mockIOrderRepository.Setup(c => c.GetById(It.IsAny<Guid>())).Returns(new Order());
            mockIOrderRepository.Setup(c => c.Remove(It.IsAny<Order>()));
            #endregion

            _controller = new OrdersController(_orderAppService, mockMapper.Object);

        }

        [When("asked to delete a order")]
        public void WhenAskedToDeleteAOrder()
        {
            _result = _controller.DeleteOrder("1972610b-dfb3-4551-bfcc-52da1d9fc254");
        }
        #endregion

        #region Get a Order by type
        [BeforeScenario("getorberbytype")]
        public void SetUpGetAOrderByType()
        {
            #region Mock Mapper
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(c => c.Map<IEnumerable<OrderModel>>(It.IsAny<IEnumerable<Order>>())).Returns(
                 new List<OrderModel>
                {
                    new OrderModel{
                    Id = "655b293d-e02b-4c2c-beeb-2cf4c404aef4",
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now.ToString(),
                    CustomerName = "Test",
                    Type = Domain.Enumerators.OrderType.Standard.ToString()
                    },
                    new OrderModel{
                    Id = "1972610b-dfb3-4551-bfcc-52da1d9fc254",
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now.ToString(),
                    CustomerName = "Frank",
                    Type = Domain.Enumerators.OrderType.Standard.ToString()
                    },new OrderModel{
                    Id = "9b87a766-2334-4d20-8d97-8e4820fe3ffd",
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now.ToString(),
                    CustomerName = "John",
                    Type = Domain.Enumerators.OrderType.Standard.ToString()
                    },
                }
                );
            #endregion

            #region Mock OrderRepository
            var mockIOrderRepository = new Mock<IOrderRepository>();
            mockIOrderRepository.Setup(c => c.GetByFilter(It.IsAny<Expression<Func<Order, bool>>>())).Returns(
                new List<Order>
                {
                    new Order{
                    Id = new Guid("655b293d-e02b-4c2c-beeb-2cf4c404aef4"),
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now,
                    CustomerName = "Test",
                    Type = Domain.Enumerators.OrderType.Standard
                    },
                    new Order{
                    Id = new Guid("1972610b-dfb3-4551-bfcc-52da1d9fc254"),
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now,
                    CustomerName = "Frank",
                    Type = Domain.Enumerators.OrderType.Standard
                    },new Order{
                    Id = new Guid("9b87a766-2334-4d20-8d97-8e4820fe3ffd"),
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now,
                    CustomerName = "John",
                    Type = Domain.Enumerators.OrderType.Standard
                    },
                });
            #endregion

            _controller = new OrdersController(_orderAppService, mockMapper.Object);

        }

        [When("asked to get a order by type")]
        public void WhenAskedToGetAOrderByType()
        {
            _result = _controller.GetOrderByType("Standard");
        }

        [Then("should return a order list with the type required")]
        public void ThenShouldReturnOrderListWithTheTypeRequired()
        {
            var okResult = _result as OkObjectResult;
            okResult?.StatusCode.Should().Be(200);
            okResult.Should().NotBeNull();
            List<OrderModel> orderList = (List<OrderModel>)okResult.Value;
            orderList?.Where(c => c.Type.Equals("Standard")).Count().Should().Be(3);
        }
        #endregion

        #region Get Orders by Customer
        [BeforeScenario("getorbersbycustomer")]
        public void SetUpGetOrdersByCustomer()
        {
            #region Mock Mapper
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(c => c.Map<IEnumerable<OrderModel>>(It.IsAny<IEnumerable<Order>>())).Returns(
                 new List<OrderModel>
                {
                    new OrderModel{
                    Id = "655b293d-e02b-4c2c-beeb-2cf4c404aef4",
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now.ToString(),
                    CustomerName = "Test",
                    Type = Domain.Enumerators.OrderType.Standard.ToString()
                    },
                    new OrderModel{
                    Id = "1972610b-dfb3-4551-bfcc-52da1d9fc254",
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now.ToString(),
                    CustomerName = "Test",
                    Type = Domain.Enumerators.OrderType.Standard.ToString()
                    },new OrderModel{
                    Id = "9b87a766-2334-4d20-8d97-8e4820fe3ffd",
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now.ToString(),
                    CustomerName = "Test",
                    Type = Domain.Enumerators.OrderType.Standard.ToString()
                    },
                }
                );
            #endregion

            #region Mock OrderRepository
            var mockIOrderRepository = new Mock<IOrderRepository>();
            mockIOrderRepository.Setup(c => c.GetByFilter(It.IsAny<Expression<Func<Order, bool>>>())).Returns(
                new List<Order>
                {
                    new Order{
                    Id = new Guid("655b293d-e02b-4c2c-beeb-2cf4c404aef4"),
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now,
                    CustomerName = "Test",
                    Type = Domain.Enumerators.OrderType.Standard
                    },
                    new Order{
                    Id = new Guid("1972610b-dfb3-4551-bfcc-52da1d9fc254"),
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now,
                    CustomerName = "Test",
                    Type = Domain.Enumerators.OrderType.Standard
                    },new Order{
                    Id = new Guid("9b87a766-2334-4d20-8d97-8e4820fe3ffd"),
                    CreatedByUsername = "admin",
                    CreatedDate = DateTime.Now,
                    CustomerName = "Test",
                    Type = Domain.Enumerators.OrderType.Standard
                    },
                });
            #endregion

            _controller = new OrdersController(_orderAppService, mockMapper.Object);

        }

        [When("asked to get orders by customer")]
        public void WhenAskedToGetOrdersByCustomer()
        {
            _result = _controller.GetByCustomer("Test");
        }

        [Then("should return a order list from customer required")]
        public void ThenShouldReturnOrderListFromCustomerRequired()
        {
            var okResult = _result as OkObjectResult;
            okResult?.StatusCode.Should().Be(200);
            okResult.Should().NotBeNull();
            List<OrderModel> orderList = (List<OrderModel>)okResult.Value;
            orderList?.Where(c => c.CustomerName.Equals("Test")).Count().Should().Be(3);
        }
        #endregion

        [Then("should return success")]
        public void ThenShouldReturnSuccess()
        {
            var okResult = _result as OkResult;
            okResult?.StatusCode.Should().Be(200);
        }
    }
}