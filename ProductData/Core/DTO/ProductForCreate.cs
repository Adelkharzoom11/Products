using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Core.DTO
{
    public class ProductForCreate
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public float price { get; set; }

        [Required, MaxLength(150), MinLength(15)]
        public string Note { get; set; }

        [Required]
        public IFormFile Img { get; set; }
        public Guid CategoryId { get; set; }
    }
}
