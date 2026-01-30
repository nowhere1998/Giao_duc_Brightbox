using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Level { get; set; }

    public int? Admin { get; set; }

    [Display(Name = "Thứ tự")]
    [Required(ErrorMessage = "Thứ tự không được để trống")]
    public int? Ord { get; set; }

    public int? Active { get; set; }

    public virtual ICollection<DocumentTypeUser> DocumentTypeUsers { get; set; } = new List<DocumentTypeUser>();

    public virtual ICollection<GroupLibraryUser> GroupLibraryUsers { get; set; } = new List<GroupLibraryUser>();

    public virtual ICollection<GroupNewsUser> GroupNewsUsers { get; set; } = new List<GroupNewsUser>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
