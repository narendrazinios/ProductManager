using Microsoft.AspNetCore.Http;
using ProductManager.Core.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.VMs
{
    public class ProductVM : ProductModel
    {
        public IList<IFormFile> Images { get; set; }
    }
}
