using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace ProductManager.Core.Models.Product
{
    public class Tenant : GuidObject
    {

        [Required]
        public string TenantName { get; set; }
        [Phone]
        public string Phone { get; set; }
        [Required]
        public string Username { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
