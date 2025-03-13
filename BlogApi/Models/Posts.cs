using System;
using System.Collections.Generic;

namespace BlogApi.Models;

public partial class Posts
{
    public int PostId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int? CategoryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Categories? Category { get; set; }

    public virtual ICollection<Tags> Tag { get; set; } = new List<Tags>();
}
