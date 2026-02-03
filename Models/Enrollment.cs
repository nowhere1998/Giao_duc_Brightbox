using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }
        public DateTime? EnrollDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? PricePaid { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public Product Product { get; set; }
    }
}
