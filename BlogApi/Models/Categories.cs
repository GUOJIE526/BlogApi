using System;
using System.Collections.Generic;

namespace BlogApi.Models;

public partial class Categories
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Posts> Posts { get; set; } = new List<Posts>();
}
