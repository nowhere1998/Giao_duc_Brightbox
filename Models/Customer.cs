using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class Customer
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Password { get; set; }
    public int Active { get; set; }

    public virtual ICollection<CommentPro> CommentPros { get; set; }
       = new List<CommentPro>();

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();


}
