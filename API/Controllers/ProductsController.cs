using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Error;
using API.Helper;
using AutoMapper;
using Core.Entity;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class ProductsController : BaseApiControler
    {

        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _proudctTypeRepo;
        
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> proudctTypeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _proudctTypeRepo = proudctTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Pagination>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec=new ProductWithFiltersForCountSpecifications(productParams);
            var totalItems= await _productRepo.CountAsync(countSpec);
            //var products = await _productRepo.ListAllAsync();            
            var products = await _productRepo.ListAsync(spec);

            var data=_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDtos>>(products);

            return Ok(new Pagination<ProductToReturnDtos>(productParams.PageSize,productParams.PageIndex,totalItems,data));

            // return products.Select(product => new ProductToReturnDtos
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Description = product.Description,
            //     Price = product.Price,
            //     PictureUrl = product.PictureUrl,
            //     ProductBrand = product.ProductBrand.Name,
            //     ProductType = product.ProductType.Name
            // }).ToList();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDtos>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            //var product = await _productRepo.GetByIdAsync(id);
            var product = await _productRepo.GetEntityWithSpec(spec);

            if(product==null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product, ProductToReturnDtos>(product);


            //return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetTypes()
        {
            return Ok(await _proudctTypeRepo.ListAllAsync());
        }
    }

    public class Pagination
    {
    }
}