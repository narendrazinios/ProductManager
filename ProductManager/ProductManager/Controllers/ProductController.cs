using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManager.Core.DAL;
using ProductManager.Core.Models.Product;
using ProductManager.VMs;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace ProductManager.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDataBaseHandler _dataBaseHandler;
        private readonly IWebHostEnvironment _env;
        public ProductController(IDataBaseHandler dataBaseHandler, IWebHostEnvironment env)
        {
            _dataBaseHandler = dataBaseHandler;
            _env = env;
        }

        // Add product
        [HttpPost]
        public async Task<ActionResult> Add([FromForm] ProductVM product)
        {
            product.Id = Guid.NewGuid();
            if (!(await _dataBaseHandler.GetAllAsync<Tenant>()).Any(x => x.Id == product.TenantId))
                throw new Exception("No Tenant Exists with this GUID.");
            var productMaster = new ProductMaster()
            {
                Id = product.Id,
                Count = product.Count,
                Description = product.Description,
                DetailsJson = product.DetailsJson,
                Name = product.Name,
                PID = product.PID,
                TenantId = product.TenantId
            };

            //if (product.Images.Count > 4)
            //    throw new Exception("Upload 4 images");
            //var fullPath = Path.Combine(_env.WebRootPath,"Images",product.Id.ToString());
            //productMaster.Images = new List<string>();

            //foreach (var image in product.Images)
            //{
            //    using (var stream = new FileStream(fullPath, FileMode.Create))
            //        image.CopyTo(stream);
            //    productMaster.Images.Add($"{fullPath}//{image.FileName}");
            //}

            await _dataBaseHandler.AddAsync(productMaster);
            return Ok(productMaster);
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

        // Product List by Name
        [HttpGet("[action]/{name}")]
        public async Task<IEnumerable<ProductMaster>> Search(string name)
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
