﻿using WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DockerSqlServer;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Product>? products = await _db.Products.ToListAsync();

            return new JsonResult(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Product? product = await _db.Products.FirstOrDefaultAsync(n => n.Id == id);

            return new JsonResult(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return new JsonResult(product.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int? id, Product product)
        {
            Product? existingProduct = await _db.Products.FirstOrDefaultAsync(n => n.Id == product.Id);

            if(existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
            }

            return new JsonResult((await _db.SaveChangesAsync()) > 0);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Product? product = await _db.Products.FirstOrDefaultAsync(n => n.Id == id);

            if(product != null)
            {
                _db.Remove(product);
            }
            
            return new JsonResult((await _db.SaveChangesAsync()) > 0);
        }
    }
}
