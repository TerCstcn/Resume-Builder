using System;
using System.Collections.Generic;

namespace TUPApp.Models;

public partial class Skill
{
    public int Id { get; set; }

    public string? Skill1 { get; set; }

    public int? StudentId { get; set; }

    public virtual Student? Student { get; set; }
}
