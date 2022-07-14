using auth.Models;

namespace auth.Data {
    public interface IFurnitureRepo {
        FurnitureModel createFurniture(FurnitureModel model);
        FurnitureModel GetById(int Id);

        FurnitureModel UpdateFurnitureStatus(int id, string status);
        List<FurnitureModel> GetFurnituresForOrder(int userId);
        List<Tuple<OrderModel, List<FurnitureModel>>> GetFurnituresForAccount(int accountId, List<OrderModel> orders);
    }
}