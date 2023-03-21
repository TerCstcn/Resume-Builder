using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TUPApp.Models;

public partial class TupContext : DbContext
{
    public TupContext()
    {
    }

    public TupContext(DbContextOptions<TupContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EducationalBackground> EducationalBackgrounds { get; set; }

    public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; }

    public virtual DbSet<Experience> Experiences { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<TrainingAttended> TrainingAttendeds { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=LAPTOP-R877VU58\\SQLEXPRESS;Database=TUP;ConnectRetryCount=0;user=sa;password=Lhester123;Persist Security Info=true;trustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EducationalBackground>(entity =>
        {
            entity.ToTable("EducationalBackground");

            entity.Property(e => e.School)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.EducationalBackgrounds)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_EducationalBackground_Student");
        });

        modelBuilder.Entity<EmergencyContact>(entity =>
        {
            entity.ToTable("Emergency Contact");

            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.EmergencyContacts)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Emergency Contact_Student");
        });

        modelBuilder.Entity<Experience>(entity =>
        {
            entity.ToTable("Experience");

            entity.Property(e => e.JobPosition)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.Experiences)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Experience_Student");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.ToTable("Skill");

            entity.Property(e => e.Skill1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Skill");

            entity.HasOne(d => d.Student).WithMany(p => p.Skills)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Skill_Student");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TrainingAttended>(entity =>
        {
            entity.ToTable("Training Attended");

            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TrainingName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Student).WithMany(p => p.TrainingAttendeds)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Training Attended_Student");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
