using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace ProductManager.Core.Models.Product
{
    public class ProductModel : GuidObject
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string PID { get; set; }
        public string Description { get; set; }

        public long Count { get; set; } = 1;
        public string DetailsJson { get; set; }
    }
}
