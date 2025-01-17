using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var Products = await _productService.GetProducts();

            if (Products == null) { return NotFound("Products not found."); }

            return Ok(Products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var Product = await _productService.GetById(id);

            if (Product == null) { return NotFound("Product not found."); }

            return Ok(Product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDTO productDto)
        {
            if (productDto == null) { return BadRequest("Invalid Data."); }

            await _productService.Add(productDto);

            return new CreatedAtRouteResult("GetProduct", new { id = productDto.Id }, productDto);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDto)
        {
            if (productDto == null || id != productDto.Id) { return BadRequest(); }

            await _productService.Update(productDto);

            return Ok(productDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var Product = await _productService.GetById(id);

            if (Product == null) { return NotFound("Product not found."); }

            await _productService.Remove(id);
            return Ok(Product);
        }
    }
}
