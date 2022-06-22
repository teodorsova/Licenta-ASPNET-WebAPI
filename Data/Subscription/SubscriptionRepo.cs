using auth.Models;

namespace auth.Data {
    public class SubcriptionRepo : ISubscriptionRepo
    {
        private readonly DatabaseContext _context;

        public SubcriptionRepo(DatabaseContext context)
        {
            _context = context;
        }

        public SubscriptionModel CreateSubscription(SubscriptionModel model)
        {
            _context.Subscriptions.Add(model);
            _context.SaveChanges();
            return model;
        }

        public void DeleteSubscription(UserModelIdDto model)
        {
            _context.Subscriptions.Remove(GetByUserId(model));
            _context.SaveChanges();
        }

        public SubscriptionModel GetByUserId(UserModelIdDto model)
        {
            return _context.Subscriptions.FirstOrDefault(s => s.Company.Id == model.Id);
        }

        public void UpdateSubscription(){
            _context.SaveChanges();
        }
    }
}