using CS.Core.Entities;

namespace CS.Infrastructure.Interfaces
{
    public interface IUow
    {
        IRepository<Product> ProductRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<CheckOut> CheckOutRepository { get; }
        IRepository<Cart> CartRepository { get; }
        void Commit();
    }
}