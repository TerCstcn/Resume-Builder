using System;
using System.Collections.Generic;

namespace TUPApp.Models;

public partial class EmergencyContact
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Contact { get; set; }

    public int? StudentId { get; set; }

    public virtual Student? Student { get; set; }
}
