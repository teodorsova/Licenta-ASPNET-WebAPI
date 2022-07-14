using auth.Models;

namespace auth.Data {
    public interface IOrderRepo {
        OrderModel createOrder(OrderModel model);
        FurnitureOrderModel createFurnitureOrder(FurnitureOrderModel model);
        public OrderModel GetById(int Id);
        List<OrderModel> GetAllOrders();
        List<OrderModel> GetAllOrdersById(int userId);
    }
}