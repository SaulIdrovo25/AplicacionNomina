using Microsoft.EntityFrameworkCore;

namespace EmpleadosReutilizacion.Models
{
    public partial class EmpleadosReutilizacionContext : DbContext
    {
        public EmpleadosReutilizacionContext()
        {
        }

        public EmpleadosReutilizacionContext(DbContextOptions<EmpleadosReutilizacionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Salary> Salaries { get; set; } = null!;
        public virtual DbSet<Title> Titles { get; set; } = null!;
        public virtual DbSet<DeptEmp> DeptEmps { get; set; } = null!;
        public virtual DbSet<DeptManager> DeptManagers { get; set; } = null!;
        public virtual DbSet<LogAuditoriaSalario> LogAuditoriaSalarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=EmpleadosReutilizacion;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptNo);
                entity.ToTable("departments");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpNo);
                entity.ToTable("employees");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.EmpNo);
                entity.ToTable("users");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.EmpNo);
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.FromDate });
                entity.ToTable("salaries");

                entity.Property(e => e.SalaryAmount).HasColumnName("salary");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.EmpNo);
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.TitleName, e.FromDate });
                entity.ToTable("titles");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.Titles)
                    .HasForeignKey(d => d.EmpNo);
            });

            modelBuilder.Entity<DeptEmp>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.DeptNo });
                entity.ToTable("dept_emp");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.DeptEmps)
                    .HasForeignKey(d => d.EmpNo);

                entity.HasOne(d => d.DeptNoNavigation)
                    .WithMany(p => p.DeptEmps)
                    .HasForeignKey(d => d.DeptNo);
            });

            modelBuilder.Entity<DeptManager>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.DeptNo });
                entity.ToTable("dept_manager");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.DeptManagers)
                    .HasForeignKey(d => d.EmpNo);

                entity.HasOne(d => d.DeptNoNavigation)
                    .WithMany(p => p.DeptManagers)
                    .HasForeignKey(d => d.DeptNo);
            });

            modelBuilder.Entity<LogAuditoriaSalario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Log_AuditoriaSalarios");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.LogAuditoriaSalarios)
                    .HasForeignKey(d => d.EmpNo);
            });
        }
    }
}
