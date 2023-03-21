using System;
using System.Collections.Generic;

namespace TUPApp.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public string? Contact { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<EducationalBackground> EducationalBackgrounds { get; } = new List<EducationalBackground>();

    public virtual ICollection<EmergencyContact> EmergencyContacts { get; } = new List<EmergencyContact>();

    public virtual ICollection<Experience> Experiences { get; } = new List<Experience>();

    public virtual ICollection<Skill> Skills { get; } = new List<Skill>();

    public virtual ICollection<TrainingAttended> TrainingAttendeds { get; } = new List<TrainingAttended>();
}
