using System;
using System.Linq;
using AutoMapper;
using CS.API.ViewModels;
using CS.Core.Entities;
using CS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CS.API.Controllers
{
    [Route("api/v1/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUow _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryController(IUow unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [Route("add_category")]
        public IActionResult AddCategory([FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.CategoryRepository.Insert(_mapper.Map<Category>(dto));
            _unitOfWork.Commit();

            var product = _unitOfWork.CategoryRepository.List().OrderByDescending(x=>x.Id).FirstOrDefault();
            return Ok(_mapper.Map<CategoryDto>(product));
        }

        [HttpGet]
        [Route("get_all_product_categories")]
        public IActionResult GetAllCategories()
        {
            var category = _unitOfWork.CategoryRepository.List();
            var mappedCategory = _mapper.ProjectTo<CategoryDto>(category).ToList();
            return Ok(mappedCategory);
        }

        [HttpDelete]
        [Route("delete_category_by_category_id/{categoryId}")]
        public IActionResult Delete([FromRoute] long categoryId)
        {
            var category = _unitOfWork.CategoryRepository.GetById(categoryId);
            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Commit();
            return Ok();
        }
    }
}