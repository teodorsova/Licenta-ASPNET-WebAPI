using auth.Data;
using auth.Helpers;
using auth.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace auth.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrderController : Controller {

        private readonly IUserRepo _userRepo;
        private readonly ISubscriptionRepo _subscriptionRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IFurnitureRepo _furnitureRepo;
        private readonly IJwtService _jwtService;

        public OrderController(IUserRepo userRepo, ISubscriptionRepo subscriptionRepo, IOrderRepo orderRepo, IFurnitureRepo furnitureRepo, IJwtService jwtService)
        {
            _userRepo = userRepo;
            _subscriptionRepo = subscriptionRepo;
            _orderRepo = orderRepo;
            _furnitureRepo = furnitureRepo;
            _jwtService = jwtService;
        }

        [HttpGet("get")]
        public IActionResult getOrders() {
            return Ok();
        }

        [HttpPost("create/order")]
        public IActionResult createOrder(OrderCreateModel model) {
            var order = new OrderModel {
                UserId = model.UserId,
                User = _userRepo.GetById(model.UserId),
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2
            };
            return Created("success", _orderRepo.createOrder(order));
        }

        [HttpPost("create/furniture")]
        public IActionResult createFurniture(FurnitureCreateDto model) {
            
            var company = _userRepo.GetByCompanyName(model.CompanyName);
            var furniture = new FurnitureModel {
                UserId = company.Id,
                Company = company,
                CompanyName = model.CompanyName,
                Price = model.Price,
                Name = model.Name,
                OrderingUserId = model.OrderingUserId
            };
            return Created("success", _furnitureRepo.createFurniture(furniture));
        }

        [HttpPost("create/furnitureOrder")]
        public IActionResult createFurnitureOrder(FurnitureOrderModelCreateDto model) {
            var furnitureOrder = new FurnitureOrderModel {
                OrderModelId = model.OrderModelId,
                Order = _orderRepo.GetById(model.OrderModelId),
                FurnitureModelId = model.FurnitureModelId,
                Furniture = _furnitureRepo.GetById(model.FurnitureModelId)
            };
            return Created("success", _orderRepo.createFurnitureOrder(furnitureOrder));
        }

        [HttpPost("post/orders")]
        public IActionResult getOrders(OrderGetDto model) {
            List<Tuple<List<FurnitureModel>, OrderModel>> furnituresForOrder = new List<Tuple<List<FurnitureModel>, OrderModel>>();
            var orderList = _orderRepo.GetAllOrdersById(model.UserId);
            foreach(OrderModel order in orderList) {
                furnituresForOrder.Add(new Tuple<List<FurnitureModel>, OrderModel> (_furnitureRepo.GetFurnituresForOrder(order.Id), order));
            }

            return Ok(furnituresForOrder);
        }

        [HttpPost("post/furnitures")]
        public IActionResult getFurnituresForOrder(FurnitureOrderModelGetDto model) {
            var list = _furnitureRepo.GetFurnituresForOrder(model.OrderModelId);
            return Ok(list);
        }

        [HttpGet("furnitures/account")]
        public IActionResult getFurnituresForAccount([FromQuery] int userId) {
            var orderList = _orderRepo.GetAllOrders();
            var listOfTuples = _furnitureRepo.GetFurnituresForAccount(userId, orderList);
            return Ok(listOfTuples);
        }

        [HttpPost("furnitures/update/status")]
        public IActionResult updateFurnitureStatus([FromQuery] int id, [FromQuery]string status) {
            return Ok(_furnitureRepo.UpdateFurnitureStatus(id, status));
        }


    }
}