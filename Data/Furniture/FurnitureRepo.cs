using auth.Models;

namespace auth.Data {
    public class FurnitureRepo : IFurnitureRepo
    {
        private readonly DatabaseContext _context;

        public FurnitureRepo(DatabaseContext context)
        {
            _context = context;
        }
        public FurnitureModel createFurniture(FurnitureModel model)
        {
            model.Status = "Processing";
            _context.Furnitures.Add(model);
            _context.SaveChanges();
            return model;
        }
        public FurnitureModel GetById(int Id)
        {
            return _context.Furnitures.FirstOrDefault(u => u.Id == Id);
        }

        public List<FurnitureModel> GetFurnituresForOrder(int orderId)
        {  
            List<FurnitureOrderModel> models = _context.FurnituresOrders.Where(o => o.OrderModelId == orderId).ToList();
                
            List<FurnitureModel> furnitureModels = new List<FurnitureModel>();
            foreach(FurnitureOrderModel fom in models) {
                FurnitureModel furnitureModel = _context.Furnitures.FirstOrDefault(f => fom.FurnitureModelId == f.Id);
                furnitureModels.Add(furnitureModel);
            }
            return furnitureModels;
        }

        public List<Tuple<OrderModel, List<FurnitureModel>>> GetFurnituresForAccount(int accountId, List<OrderModel> orders) {
            List<Tuple<OrderModel, List<FurnitureModel>>> returnData = new List<Tuple<OrderModel, List<FurnitureModel>>>();

            foreach(OrderModel order in orders) {
                 List<FurnitureModel> furnitureModels = GetFurnituresForOrder(order.Id);
                 order.User = _context.Users.FirstOrDefault(u=>u.Id == order.UserId);
                 order.User.Password = "";
                 order.User.Role="";
                 furnitureModels = furnitureModels.FindAll(furniture=> furniture.UserId == accountId);
                 if(furnitureModels.Count > 0) {
                    returnData.Add(new Tuple<OrderModel, List<FurnitureModel>>(order, furnitureModels));
                 }
            }
            return returnData;
        }

        public FurnitureModel UpdateFurnitureStatus(int id, string status)
        {
            FurnitureModel furniture = _context.Furnitures.FirstOrDefault(f => f.Id == id);
            furniture.Status = status;
            _context.SaveChanges();
            return furniture;
        }
    }
}