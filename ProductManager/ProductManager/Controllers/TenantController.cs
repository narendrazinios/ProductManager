using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductManager.Core.DAL;
using ProductManager.Core.Models.Product;
using ProductManager.VMs;
using Microsoft.AspNetCore.Hosting;

namespace ProductManager.Controllers
{
    [Route("api/[Controller]")]
    public class TenantController : ControllerBase
    {
        private readonly IDataBaseHandler _dataBaseHandler;
        public TenantController(IDataBaseHandler dataBaseHandler)
        {
            _dataBaseHandler = dataBaseHandler;
        }

        // Add product
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] Tenant tenant)
        {
            if (tenant.Id == null)
                tenant.Id = Guid.NewGuid();
            await _dataBaseHandler.AddAsync(tenant);
            return Ok(tenant);
        }


        // Update Tenant from web
        [HttpPut]
        public async Task<ActionResult> UpdateTenant([FromBody] Tenant tenant)
        {
            return Ok(await _dataBaseHandler.UpdateAsync(tenant));
        }


        // Tenant List
        [HttpGet]
        public async Task<IEnumerable<Tenant>> Get()
        {
            return await _dataBaseHandler.GetAllAsync<Tenant>();
        }


        // GET api/<TenantController>/5
        [HttpGet("{id}")]
        public async Task<Tenant> Get(Guid id)
        {
            return await _dataBaseHandler.FirstOrDefaultAsync<Tenant>(x => x.Id == id);
        }



        // DELETE api/<TenantController>/5
        [HttpDelete("{id}")]
        public async void Delete(Guid id)
        {
            await _dataBaseHandler.DeleteRangeAsync<Tenant>(x => x.Id == id);
        }
    }
}
