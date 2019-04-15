using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Modelo
{
    public partial class ModelContext : DbContext
    {
        private readonly string _connStr;
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<JobHistory> JobHistory { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseOracle("Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = DESKTOP-4VOSOU3)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = XE)));PASSWORD=123;PERSIST SECURITY INFO=True;POOLING=False;USER ID=HR");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:DefaultSchema", "HR");

            modelBuilder.Entity<Countries>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("COUNTRY_C_ID_PK");

                entity.ToTable("COUNTRIES");

                entity.HasIndex(e => e.CountryId)
                    .HasName("COUNTRY_C_ID_PK")
                    .IsUnique();

                entity.Property(e => e.CountryId)
                    .HasColumnName("COUNTRY_ID")
                    .HasColumnType("CHAR(2)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CountryName)
                    .HasColumnName("COUNTRY_NAME")
                    .HasColumnType("VARCHAR2(40)");

                entity.Property(e => e.RegionId)
                    .HasColumnName("REGION_ID")
                    .HasColumnType("NUMBER");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Countries)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("COUNTR_REG_FK");
            });

            modelBuilder.Entity<Departments>(entity =>
            {
                entity.HasKey(e => e.DepartmentId)
                    .HasName("DEPT_ID_PK");

                entity.ToTable("DEPARTMENTS");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("DEPT_ID_PK")
                    .IsUnique();

                entity.HasIndex(e => e.LocationId)
                    .HasName("DEPT_LOCATION_IX");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DEPARTMENT_ID")
                    .HasColumnType("NUMBER(4)");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasColumnName("DEPARTMENT_NAME")
                    .HasColumnType("VARCHAR2(30)");

                entity.Property(e => e.LocationId)
                    .HasColumnName("LOCATION_ID")
                    .HasColumnType("NUMBER(4)");

                entity.Property(e => e.ManagerId)
                    .HasColumnName("MANAGER_ID")
                    .HasColumnType("NUMBER(6)");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("DEPT_LOC_FK");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("DEPT_MGR_FK");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("EMP_EMP_ID_PK");

                entity.ToTable("EMPLOYEES");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("EMP_DEPARTMENT_IX");

                entity.HasIndex(e => e.Email)
                    .HasName("EMP_EMAIL_UK")
                    .IsUnique();

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("EMP_EMP_ID_PK")
                    .IsUnique();

                entity.HasIndex(e => e.JobId)
                    .HasName("EMP_JOB_IX");

                entity.HasIndex(e => e.ManagerId)
                    .HasName("EMP_MANAGER_IX");

                entity.HasIndex(e => new { e.LastName, e.FirstName })
                    .HasName("EMP_NAME_IX");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("EMPLOYEE_ID")
                    .HasColumnType("NUMBER(6)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CommissionPct)
                    .HasColumnName("COMMISSION_PCT")
                    .HasColumnType("NUMBER(2,2)");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DEPARTMENT_ID")
                    .HasColumnType("NUMBER(4)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasColumnType("VARCHAR2(25)");

                entity.Property(e => e.FirstName)
                    .HasColumnName("FIRST_NAME")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.HireDate)
                    .HasColumnName("HIRE_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.JobId)
                    .IsRequired()
                    .HasColumnName("JOB_ID")
                    .HasColumnType("VARCHAR2(10)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("LAST_NAME")
                    .HasColumnType("VARCHAR2(25)");

                entity.Property(e => e.ManagerId)
                    .HasColumnName("MANAGER_ID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("PHONE_NUMBER")
                    .HasColumnType("VARCHAR2(20)");

                entity.Property(e => e.Salary)
                    .HasColumnName("SALARY")
                    .HasColumnType("NUMBER(8,2)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("EMP_DEPT_FK");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("EMP_JOB_FK");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.InverseManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("EMP_MANAGER_FK");
            });

            modelBuilder.Entity<JobHistory>(entity =>
            {
                entity.HasKey(e => new { e.StartDate, e.EmployeeId })
                    .HasName("JHIST_EMP_ID_ST_DATE_PK");

                entity.ToTable("JOB_HISTORY");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("JHIST_DEPARTMENT_IX");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("JHIST_EMPLOYEE_IX");

                entity.HasIndex(e => e.JobId)
                    .HasName("JHIST_JOB_IX");

                entity.HasIndex(e => new { e.EmployeeId, e.StartDate })
                    .HasName("JHIST_EMP_ID_ST_DATE_PK")
                    .IsUnique();

                entity.Property(e => e.StartDate)
                    .HasColumnName("START_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("EMPLOYEE_ID")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DEPARTMENT_ID")
                    .HasColumnType("NUMBER(4)");

                entity.Property(e => e.EndDate)
                    .HasColumnName("END_DATE")
                    .HasColumnType("DATE");

                entity.Property(e => e.JobId)
                    .IsRequired()
                    .HasColumnName("JOB_ID")
                    .HasColumnType("VARCHAR2(10)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.JobHistory)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("JHIST_DEPT_FK");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.JobHistory)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("JHIST_EMP_FK");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobHistory)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("JHIST_JOB_FK");
            });

            modelBuilder.Entity<Jobs>(entity =>
            {
                entity.HasKey(e => e.JobId)
                    .HasName("JOB_ID_PK");

                entity.ToTable("JOBS");

                entity.HasIndex(e => e.JobId)
                    .HasName("JOB_ID_PK")
                    .IsUnique();

                entity.Property(e => e.JobId)
                    .HasColumnName("JOB_ID")
                    .HasColumnType("VARCHAR2(10)")
                    .ValueGeneratedNever();

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasColumnName("JOB_TITLE")
                    .HasColumnType("VARCHAR2(35)");

                entity.Property(e => e.MaxSalary)
                    .HasColumnName("MAX_SALARY")
                    .HasColumnType("NUMBER(6)");

                entity.Property(e => e.MinSalary)
                    .HasColumnName("MIN_SALARY")
                    .HasColumnType("NUMBER(6)");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("LOC_ID_PK");

                entity.ToTable("LOCATIONS");

                entity.HasIndex(e => e.City)
                    .HasName("LOC_CITY_IX");

                entity.HasIndex(e => e.CountryId)
                    .HasName("LOC_COUNTRY_IX");

                entity.HasIndex(e => e.LocationId)
                    .HasName("LOC_ID_PK")
                    .IsUnique();

                entity.HasIndex(e => e.StateProvince)
                    .HasName("LOC_STATE_PROVINCE_IX");

                entity.Property(e => e.LocationId)
                    .HasColumnName("LOCATION_ID")
                    .HasColumnType("NUMBER(4)");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("CITY")
                    .HasColumnType("VARCHAR2(30)");

                entity.Property(e => e.CountryId)
                    .HasColumnName("COUNTRY_ID")
                    .HasColumnType("CHAR(2)");

                entity.Property(e => e.PostalCode)
                    .HasColumnName("POSTAL_CODE")
                    .HasColumnType("VARCHAR2(12)");

                entity.Property(e => e.StateProvince)
                    .HasColumnName("STATE_PROVINCE")
                    .HasColumnType("VARCHAR2(25)");

                entity.Property(e => e.StreetAddress)
                    .HasColumnName("STREET_ADDRESS")
                    .HasColumnType("VARCHAR2(40)");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("LOC_C_ID_FK");
            });

            modelBuilder.Entity<Regions>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .HasName("REG_ID_PK");

                entity.ToTable("REGIONS");

                entity.HasIndex(e => e.RegionId)
                    .HasName("REG_ID_PK")
                    .IsUnique();

                entity.Property(e => e.RegionId)
                    .HasColumnName("REGION_ID")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.RegionName)
                    .HasColumnName("REGION_NAME")
                    .HasColumnType("VARCHAR2(25)");
            });

            modelBuilder.HasSequence("DEPARTMENTS_SEQ");

            modelBuilder.HasSequence("EMPLOYEES_SEQ");

            modelBuilder.HasSequence("LOCATIONS_SEQ");
        }
    }
}
