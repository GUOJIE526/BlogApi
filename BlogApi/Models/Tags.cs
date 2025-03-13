using System;
using System.Collections.Generic;

namespace BlogApi.Models;

public partial class Tags
{
    public int TagId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Posts> Post { get; set; } = new List<Posts>();
}
