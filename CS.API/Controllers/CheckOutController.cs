using System;
using System.Linq;
using AutoMapper;
using CS.API.ViewModels;
using CS.Core.Entities;
using CS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CS.API.Controllers
{
    [Route("api/v1/checkout")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private readonly IUow _unitOfWork;
        private readonly IMapper _mapper;

        public CheckOutController(IUow unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("checkout")]
        public IActionResult CheckOut([FromBody] CheckOutDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.CheckOutRepository.Insert(_mapper.Map<CheckOut>(dto));
            _unitOfWork.Commit();

            var id = _unitOfWork.CheckOutRepository.List().OrderByDescending(x => x.Id).FirstOrDefault().Id;
            _unitOfWork.CartRepository.Insert(dto.Cart.Select(x => new Cart
            {
                CheckOutId = id,
                ProductId = int.Parse(x.Id),
                Amount = x.Amount,
                Image = x.Image,
                Name = x.Name,
                NewQuantity = x.NewQuantity,
                OldQuantity = x.OldQuantity,
                TotalAmount = x.TotalAmount

            }).FirstOrDefault());

            foreach (var item in dto.Cart)
            {
                var product = _unitOfWork.ProductRepository.GetById(long.Parse(item.Id));
                if (product != null)
                {
                    product.Quantity -= item.NewQuantity;
                    _unitOfWork.ProductRepository.Update(_mapper.Map<Product>(product));
                }
            }

            _unitOfWork.Commit();
            return Ok(true);
        }
    }
}