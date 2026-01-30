using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models;

public partial class GroupLibrary
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Tag { get; set; }

    public string? Level { get; set; }

    public string? Image { get; set; }

    [Display(Name = "Thứ tự")]
    [Required(ErrorMessage = "Thứ tự không được để trống")]
    public int? Ord { get; set; }

    public int? Active { get; set; }

    public string? Lang { get; set; }

    public int? Type { get; set; }

    public DateTime? Date { get; set; }

    public int? ShowIndex { get; set; }

    public int? GroupId { get; set; }

    public virtual ICollection<GroupLibraryUser> GroupLibraryUsers { get; set; } = new List<GroupLibraryUser>();

    public virtual ICollection<Library> Libraries { get; set; } = new List<Library>();
}
