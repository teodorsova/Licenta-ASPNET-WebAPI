using auth.Models;

namespace auth.Data {
    public class OrderRepo : IOrderRepo
    {
        private readonly DatabaseContext _context;

        public OrderRepo(DatabaseContext context)
        {
            _context = context;
        }

        public FurnitureOrderModel createFurnitureOrder(FurnitureOrderModel model)
        {
            _context.FurnituresOrders.Add(model);
            _context.SaveChanges();
            return model;
        }

        public OrderModel createOrder(OrderModel model)
        {
            model.Status = "Awaiting confirmation...";
            _context.Orders.Add(model);
            _context.SaveChanges();
            return model;
        }

        public OrderModel GetById(int Id)
        {
            return _context.Orders.FirstOrDefault(u => u.Id == Id);
        }

        public List<OrderModel> GetAllOrders(int userId)
        {
            return _context.Orders.Where(order => order.UserId == userId).ToList();
        }
    }
}