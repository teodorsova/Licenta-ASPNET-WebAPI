using auth.Models;

namespace auth.Data {
    public interface IFurnitureRepo {
        FurnitureModel createFurniture(FurnitureModel model);
        FurnitureModel GetById(int Id);
        List<FurnitureModel> GetFurnituresForOrder(int userId);
    }
}