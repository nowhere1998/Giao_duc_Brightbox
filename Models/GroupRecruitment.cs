using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class GroupRecruitment
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(256)]
        public string? Name { get; set; }

        [MaxLength(256)]
        public string? Tag { get; set; }

        public int Status { get; set; }  // 1: hiển thị, 0: ẩn

        // Navigation property
        public ICollection<Recruitment>? Recruitments { get; set; } = new List<Recruitment>();
    }
}
