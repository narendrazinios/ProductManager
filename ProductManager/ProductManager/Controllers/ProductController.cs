using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManager.Core.DAL;
using ProductManager.Core.Models.Product;

namespace ProductManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDataBaseHandler _dataBaseHandler;
        public ProductController(IDataBaseHandler dataBaseHandler)
        {
            _dataBaseHandler = dataBaseHandler;
        }

        // Add product
        [HttpPost]
        public async void Add([FromBody] ProductMaster product)
        {
            product.Id = Guid.NewGuid();
            await _dataBaseHandler.AddAsync(product);
        }

        // Update Product from web
        [HttpPut]
        public async void UpdateProduct([FromBody] ProductMaster product)
        {
            await _dataBaseHandler.UpdateAsync(product);
        }

        // Update only PID, Count of Product for mobile
        [HttpPut("{id}")]
        public async void UpdateProduct(Guid id, [FromBody] ProductMaster product)
        {
            var dbProduct = await _dataBaseHandler.FirstOrDefaultAsync<ProductMaster>(p => p.Id == id);
            if (dbProduct.Name != product.Name || dbProduct.Images != product.Images || product.Description != dbProduct.Description)
                throw new UnauthorizedAccessException(message: "you can only update the 'Count' and 'Product Id'");
            await _dataBaseHandler.UpdateAsync(product);
        }

        // Product List
        [HttpGet]
        public async Task<IEnumerable<ProductMaster>> Get()
        {
            return await _dataBaseHandler.GetAllAsync<ProductMaster>();
        }


        // GET api/<ProductController>/5
        [HttpGet("{pId}")]
        public async Task<ProductMaster> Get(string pId)
        {
            return await _dataBaseHandler.FirstOrDefaultAsync<ProductMaster>(x => x.PID == pId);
        }

#nullable enable
        // Product List by Name
        [HttpGet("[action]/{name?}")]
        public async Task<IEnumerable<ProductMaster>> Search(string? name)
        {
            if (string.IsNullOrEmpty(name))
                return await _dataBaseHandler.GetAllAsync<ProductMaster>();
            return await _dataBaseHandler.GetAsync<ProductMaster>(a => a.Name.Contains(name));
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{pId}")]
        public async void Delete(string pid)
        {
            await _dataBaseHandler.DeleteRangeAsync<ProductMaster>(x => x.PID == pid);
        }
    }
}
