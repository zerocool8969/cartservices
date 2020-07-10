using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CS.API.ViewModels;
using CS.Core.Entities;
using CS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CS.API.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUow _unitOfWork;
        private readonly IMapper _mapper;
        public ProductController(IUow unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Route("get_products_by_categoryId/{categoryId}")]
        public IActionResult GetProductsByCategoryId(long categoryId)
        {
            var products = _unitOfWork.ProductRepository.List(x => x.CategoryId == categoryId);
            var mappedProducts = _mapper.ProjectTo<ProductDto>(products).ToList();
            return Ok(mappedProducts);
        }

        [HttpGet]
        [Route("get_products_product_id/{productId}")]
        public IActionResult GetProductsByProductId(long productId)
        {
            var product = _unitOfWork.ProductRepository.GetById(productId);
            var mappedProduct = _mapper.Map<ProductDto>(product);
            return Ok(mappedProduct);
        }

        [HttpGet]
        [Route("get_all_products")]
        public IActionResult GetAllProducts()
        {
            var products = _unitOfWork.ProductRepository.List();
            var mappedProducts = _mapper.ProjectTo<ProductDto>(products).ToList();
            return Ok(mappedProducts);
        }

        [HttpPost]
        [Route("add_product")]
        public IActionResult AddProduct([FromBody] ProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.ProductRepository.Insert(_mapper.Map<Product>(dto));
            _unitOfWork.Commit();
            
            var product = _unitOfWork.ProductRepository.List().OrderByDescending(x => x.Id).FirstOrDefault();
            return Ok(_mapper.Map<ProductDto>(product));
        }

        [HttpDelete]
        [Route("delete_product_by_productId/{productId}")]
        public IActionResult DeleteProductByProductId(int productId)
        {
            var product = _unitOfWork.ProductRepository.GetById(productId);
            _unitOfWork.ProductRepository.Delete(product);
            return Ok();
        }

        [HttpGet]
        [Route("get_my_orders/{userId}")]
        public IActionResult GetMyOrders(string userId)
        {
            List<MyOrdersDto> myOrders = new List<MyOrdersDto>();
            var list = _unitOfWork.CheckOutRepository.List(x => x.UserId == userId);
            foreach (var item in list)
            {
                var newlist = _unitOfWork.CartRepository.List(x => x.CheckOutId == item.Id);
                foreach (var result in newlist)
                {
                    MyOrdersDto myOrdersDto = new MyOrdersDto
                    {
                        Amount = result.Amount,
                        Image = result.Image,
                        ProductName = result.Name,
                        QuantityBought = result.NewQuantity
                    };
                    myOrders.Add(myOrdersDto);
                }
            }
            return Ok(myOrders);
        }
    }
}