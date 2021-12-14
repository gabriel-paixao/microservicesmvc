﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Store.Web.Models;
using Store.Web.Services.IServices;

namespace Store.Web.Controllers;
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> ProductIndex()
    {
        var list = new List<ProductDto>();
        var response = await _productService.GetAllProductsAsync<ResponseDto>();
        if (response != null && response.IsSuccess)
        {
            list = response.GetResult<List<ProductDto>>();
        }
        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> ProductCreate()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductCreate(ProductDto model)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.CreateProductAsync<ResponseDto>(model);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ProductEdit(int productId)
    {
        var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
        if (response != null && response.IsSuccess)
        {
            var product = response.GetResult<ProductDto>();
            return View(product);
        }
        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductEdit(ProductDto model)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.UpdateProductAsync<ResponseDto>(model);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ProductDelete(int productId)
    {
        var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
        if (response != null && response.IsSuccess)
        {
            var product = response.GetResult<ProductDto>();
            return View(product);
        }
        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductDelete(ProductDto model)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.DeleteProductAsync<ResponseDto>(model.ProductId);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }
        return View(model);
    }
}
