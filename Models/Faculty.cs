using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Faculty
    {
        [Key]
        public int FacultyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Active { get; set; }
        public string Image { get; set; }

        // ===== Navigation =====
        public ICollection<ProFaculty> ProFaculties { get; set; }
    }
}
