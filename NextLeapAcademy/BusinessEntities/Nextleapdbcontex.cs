using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NextLeapAcademy.BusinessEntities;

public partial class Nextleapdbcontex : DbContext
{
    public Nextleapdbcontex()
    {
    }

    public Nextleapdbcontex(DbContextOptions<Nextleapdbcontex> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DC-MUHAMMEDSUHA;Initial Catalog=Nextleap;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__37E005DB9D0DB6CE");

            entity.HasIndex(e => e.Title, "UQ__Courses__2CB664DC284236FD").IsUnique();

            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Nationality>(entity =>
        {
            entity.HasKey(e => e.NationId).HasName("PK__National__3260523B916F1594");

            entity.ToTable("Nationality");

            entity.HasIndex(e => e.NationName, "UQ__National__99798A17BDA1C437").IsUnique();

            entity.Property(e => e.NationId).HasColumnName("nation_Id");
            entity.Property(e => e.NationName).HasMaxLength(25);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__A2F4E98C8A7B6071");

            entity.HasIndex(e => e.RollNumber, "UQ__Students__E9F06F161F3CF903").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("Student_Id");
            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("mobile_number");
            entity.Property(e => e.NationId).HasColumnName("nation_Id");
            entity.Property(e => e.RollNumber).HasMaxLength(20);
            entity.Property(e => e.StudentName).HasMaxLength(50);

            entity.HasOne(d => d.Course).WithMany(p => p.Students)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Course");

            entity.HasOne(d => d.Nation).WithMany(p => p.Students)
                .HasForeignKey(d => d.NationId)
                .HasConstraintName("FK_nation");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
