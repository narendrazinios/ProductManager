using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;

namespace ProductManager.Core.Models
{
   public class GuidObject: IGuidObject
    {
        [Key]
       public virtual Guid Id { get; set; }
    }
}
