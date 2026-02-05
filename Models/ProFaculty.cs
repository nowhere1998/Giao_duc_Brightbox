using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class ProFaculty
    {
        [Key]
        public int ProFacultyID { get; set; }

        public int ProductId { get; set; }
        public int FacultyID { get; set; }

        // ===== Navigation =====
        public Product Product { get; set; }
        public Faculty Faculty { get; set; }
    }
}
