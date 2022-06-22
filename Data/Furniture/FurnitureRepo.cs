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
    }
}