using System.ComponentModel.DataAnnotations;

namespace ProductsDomain
{
    public class Category
    {
        [Required]
        public Guid Id { get; set; }
        [Required , MaxLength(50)]
        public string Name { get; set; }
        public List<Product> Products { get; set; }=new List<Product>();
    }
}
