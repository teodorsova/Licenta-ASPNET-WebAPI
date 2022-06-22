using auth.Models;

namespace auth.Data {
    public interface ISubscriptionRepo {
        SubscriptionModel CreateSubscription(SubscriptionModel model);
        SubscriptionModel GetByUserId(UserModelIdDto model);

        void DeleteSubscription(UserModelIdDto model);
        void UpdateSubscription();
    }
}