using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Practical_Exam_Hunghhth2203033.Entities;

public partial class PracticalExamContext : DbContext
{
    public static String connectionString;
    public PracticalExamContext()
    {
    }

    public PracticalExamContext(DbContextOptions<PracticalExamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Projectemployee> Projectemployees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\MSSQLSERVER01;Initial Catalog=Practical_Exam;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__employee__C52E0BA88440DF16");

            entity.ToTable("employees");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EmployeeDepartment)
                .HasMaxLength(255)
                .HasColumnName("employee_department");
            entity.Property(e => e.EmployeeDob)
                .HasColumnType("datetime")
                .HasColumnName("employee_DOB");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(255)
                .HasColumnName("employee_name");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__projects__BC799E1F64ACE4CD");

            entity.ToTable("projects");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.ProjectEndDate)
                .HasColumnType("datetime")
                .HasColumnName("project_end_date");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(255)
                .HasColumnName("project_name");
            entity.Property(e => e.ProjectStartDate)
                .HasColumnType("datetime")
                .HasColumnName("project_start_date");
        });

        modelBuilder.Entity<Projectemployee>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("projectemployees");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Tasks)
                .HasMaxLength(255)
                .HasColumnName("tasks");

            entity.HasOne(d => d.Employee).WithMany()
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__projectem__emplo__3A81B327");

            entity.HasOne(d => d.Project).WithMany()
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__projectem__proje__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
