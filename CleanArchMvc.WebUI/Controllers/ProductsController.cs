﻿using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService=categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryId =
                new SelectList(await _categoryService.GetCategories(), "Id", "Name");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var productDto = await _productService.GetById(id);

            if (productDto == null) return NotFound();

            var categories = await _categoryService.GetCategories();

            ViewBag.CategoryiD = new SelectList(categories, "Id", "Name",
                productDto.CategoryId);

            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var productDto = await _productService.GetById(id);
            if (productDto == null) return NotFound();

            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                await _productService.Add(product);
                return RedirectToAction("index");
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                await _productService.Update(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}