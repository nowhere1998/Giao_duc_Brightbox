using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class Recruitment
    {
        [Key]
        public int RecruitmentId { get; set; }

        [MaxLength(255)]
        public string? JobTitle { get; set; }

        public string? JobDescription { get; set; }

        [MaxLength(255)]
        public string? CompanyName { get; set; }

        [MaxLength(255)]
        public string? CompanyAddress { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(100)]
        public string? Salary { get; set; }

        public int? GroupRecruitmentId { get; set; }

        public DateTime Date { get; set; }

        public int Status { get; set; }

        // Navigation property
        [ForeignKey("GroupRecruitmentId")]
        public GroupRecruitment? GroupRecruitment { get; set; }
    }
}
