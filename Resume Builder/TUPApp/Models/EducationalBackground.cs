using System;
using System.Collections.Generic;

namespace TUPApp.Models;

public partial class EducationalBackground
{
    public int Id { get; set; }

    public string? School { get; set; }

    public int? Year { get; set; }

    public int? StudentId { get; set; }

    public virtual Student? Student { get; set; }
}
