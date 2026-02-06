using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Faculty
    {
        [Key]
        public int FacultyID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Active { get; set; } = 1;
        public string? Image { get; set; }


        // 🔥 Navigation property (1 khoa – nhiều product)
        public ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
