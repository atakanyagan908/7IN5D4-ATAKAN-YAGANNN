using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace _7IN5D4_ATAKAN_YAGAN.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _productService.GetAll();
                Response.Headers.Add("Cache-Control", "no-store");
                Response.Headers.Add("Content-Type", "application/json");
                return Ok(result);

            }
            catch (Exception e)
            {
                return StatusCode(500, "Bir hata oluştu: " + e.Message);

            }
            
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _productService.Add(product);
                return StatusCode(201);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Bir hata oluştu: " + e.Message);
            }
            
            
            
        }

        [Authorize]
        [HttpPut("{id}")]       
        public IActionResult Update(Product product)
        {
            try
            {
                _productService.Update(product);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Bir hata oluştu: " + e.Message);

            }

            
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Product product)
        {
            try
            {
                _productService.Delete(product);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Bir hata oluştu: " + e.Message);

            }

            
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _productService.GetById(id);
                Response.Headers.Add("Cache-Control", "no-store");
                Response.Headers.Add("Content-Type", "application/json");
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Bir hata oluştu: " + e.Message);

            }


        }
    }
}
