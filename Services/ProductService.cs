using AutoMapper;
using Dto;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<DtoResultProduct> GetProducts(
     [FromQuery] int position,
     [FromQuery] int skip,
     [FromQuery] string? desc,
     [FromQuery] int? minPrice,
     [FromQuery] int? maxPrice,
     [FromQuery] int?[] categoryIds,
     [FromQuery] int?[] styleIds)
        {
            var productResult = await _productRepository.GetProducts(position, skip, desc, minPrice, maxPrice, categoryIds, styleIds);
            var products = _mapper.Map<List<Product>, List<DtoProductIdNameCategoryPriceDescImage>>(productResult.Items);
            var response = new DtoResultProduct(
                products,
                productResult.TotalCount
            );
            return response;
        }

        public async Task<DtoProductIdNameCategoryPriceDescImage> AddNewProduct(DtoProductNameDescriptionPriceStockCategoryIdIsActiveStyleIds productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            productEntity.ProductStyles = productDto.ProductStyles.Select(id => new ProductStyle
            {
                StyleId = id.StyleId
            }).ToList();

            var savedProduct = await _productRepository.AddNewProduct(productEntity);
            return _mapper.Map<DtoProductIdNameCategoryPriceDescImage>(savedProduct);
        }

        public async Task<DtoProductIdNameCategoryPriceDescImage> GetById(
        int id)
        {
            Product product = await _productRepository.GetById(id);
            return _mapper.Map<Product, DtoProductIdNameCategoryPriceDescImage>(product);

        }

        public async Task<DtoProductIdNameCategoryPriceDescImage> Delete(int id)
        {
            var savedProduct = await _productRepository.Delete(id);
            return _mapper.Map<DtoProductIdNameCategoryPriceDescImage>(savedProduct);

        }
    }
}

