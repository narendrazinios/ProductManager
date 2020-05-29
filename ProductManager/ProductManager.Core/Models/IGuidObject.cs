using System;
using System.Collections.Generic;
using System.Text;

namespace ProductManager.Core.Models
{
  public  interface IGuidObject
    {
        Guid Id { get; set; }
    }
}
