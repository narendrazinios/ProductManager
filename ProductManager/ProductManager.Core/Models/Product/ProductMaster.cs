using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManager.Core.Models.Product
{
    public class ProductMaster : ProductModel
    {
        public IList<string> Images { get; set; }
        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

    }
}
