using System;
using System.Collections.Generic;

namespace TUPApp.Models;

public partial class Experience
{
    public int Id { get; set; }

    public string? JobPosition { get; set; }

    public int? Year { get; set; }

    public int? StudentId { get; set; }

    public virtual Student? Student { get; set; }
}
