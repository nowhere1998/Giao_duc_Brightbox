using System;
using System.Collections.Generic;

namespace MyShop.Models;

public partial class UserGroup
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? GroupId { get; set; }

    public int? SetInsert { get; set; }

    public int? SetEdit { get; set; }

    public int? SetDelete { get; set; }
}
