﻿using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            //var productsEntity = await _productRepository.GetProductsAsync();
            //return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
            var productsQuery = new GetProductsQuery();

            if (productsQuery == null)
                throw new Exception($"entity could not be loaded");

            var result = await _mediator.Send(productsQuery);

            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        //public async Task<ProductDTO> GetById(int? id)
        //{
        //    var productEntity = await _productRepository.GetByIdAsync(id);
        //    return _mapper.Map<ProductDTO>(productEntity);
        //}

        //public async Task<ProductDTO> GetProductCategory(int? id)
        //{
        //    var productEntity = await _productRepository.GetProductCategoryAsync(id);
        //    return _mapper.Map<ProductDTO>(productEntity);
        //}

        //public async Task Add(ProductDTO productDto)
        //{
        //    var productEntity = _mapper.Map<Product>(productDto);
        //    await _productRepository.CreateAsync(productEntity);
        //}

        //public async Task Update(ProductDTO productDto)
        //{
        //    var productEntity = _mapper.Map<Product>(productDto);
        //    await _productRepository.UpdateAsync(productEntity);
        //}

        //public async Task Remove(int? id)
        //{
        //    var productEntity = _productRepository.GetByIdAsync(id).Result;
        //    await _productRepository.RemoveAsync(productEntity);
        //}
    }
}