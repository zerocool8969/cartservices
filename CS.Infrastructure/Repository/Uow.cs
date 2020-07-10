using CS.Core.Entities;
using CS.Core.Exceptions;
using CS.Infrastructure.Context;
using CS.Infrastructure.Interfaces;
using System;

namespace CS.Infrastructure.Repository
{
    public class Uow : IUow
    {
        private readonly CartContext _cartContext;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<CheckOut> _checkoutRepository;
        private readonly IRepository<Cart> _cartRepository;

        public Uow(CartContext cartContext, IRepository<Product> productRepository, IRepository<Category> categoryRepository, IRepository<CheckOut> checkoutRepository, IRepository<Cart> cartRepository)
        {
            _cartContext = cartContext ?? throw new ArgumentNullException(nameof(cartContext));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _checkoutRepository = checkoutRepository ?? throw new ArgumentNullException(nameof(checkoutRepository));
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public IRepository<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    throw new NotFoundException("Product repository not found.");
                }
                return _productRepository;
            }
        }

        public IRepository<Category> CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    throw new NotFoundException("Category repository not found.");
                }
                return _categoryRepository;
            }
        }

        public IRepository<CheckOut> CheckOutRepository
        {
            get
            {
                if (_checkoutRepository == null)
                {
                    throw new NotFoundException("CheckOut repository not found.");
                }
                return _checkoutRepository;
            }
        }

        public IRepository<Cart> CartRepository
        {
            get
            {
                if (_cartRepository == null)
                {
                    throw new NotFoundException("Cart repository not found.");
                }
                return _cartRepository;
            }
        }

        public void Commit()
        {
            _cartContext.SaveChanges();
        }
    }
}
