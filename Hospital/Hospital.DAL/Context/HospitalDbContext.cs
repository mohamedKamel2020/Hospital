using Hospital.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.DAL.Context;

public partial class HospitalDbContext : DbContext
{
    public HospitalDbContext()
    {
    }

    public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
        : base(options)
    {
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseNpgsql("Host=localhost;Database=HospitalDB;Username=postgres;Password=postgres1");

    public virtual DbSet<Accountant> Accountants { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Depatment> Depatments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Doctorwritetestreport> Doctorwritetestreports { get; set; }

    public virtual DbSet<Medicalrecord> Medicalrecords { get; set; }

    public virtual DbSet<Nurse> Nurses { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Recordtreatment> Recordtreatments { get; set; }

    public virtual DbSet<Testreporttreatment> Testreporttreatments { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accountant>(entity =>
        {
            entity.HasKey(e => e.Accountantid).HasName("accountant_pkey");


            entity.ToTable("accountant");

            entity.Property(e => e.Accountantid).HasColumnName("accountantid");
            entity.Property(e => e.Accountantname)
                .HasMaxLength(50)
                .HasColumnName("accountantname");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.ApartmentBlockNo).HasColumnName("apartment_block_no");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .HasColumnName("city");
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .HasColumnName("phone");
            entity.Property(e => e.Salary)
                .HasPrecision(8, 2)
                .HasColumnName("salary");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .HasColumnName("sex");
            entity.Property(e => e.Shift)
                .HasMaxLength(10)
                .HasColumnName("shift");
            entity.Property(e => e.Street)
                .HasMaxLength(20)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Bill>(entity =>
        {

            entity.HasKey(e => e.Billid).HasName("bill_pkey");


            entity.ToTable("bill");

            entity.Property(e => e.Billid).HasColumnName("billid");
            entity.Property(e => e.Accid).HasColumnName("accid");
            entity.Property(e => e.Hasinsurance).HasColumnName("hasinsurance");
            entity.Property(e => e.Patid).HasColumnName("patid");
            entity.Property(e => e.Totalbill)
                .HasPrecision(10, 2)
                .HasColumnName("totalbill");

            entity.HasOne(d => d.Acc).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Accid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_bill_accountant");

            entity.HasOne(d => d.Pat).WithMany(p => p.Bills)
                .HasForeignKey(d => d.Patid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_bill_patient");
        });

        modelBuilder.Entity<Depatment>(entity =>
        {
            entity.HasKey(e => e.Departmentid).HasName("depatment_pkey");


            entity.ToTable("depatment");
            entity.Property(e => e.fileicon);
            entity.Property(e => e.Departmentid).HasColumnName("departmentid");
            entity.Property(e => e.Departmentlocation)
                .HasMaxLength(50)
                .HasColumnName("departmentlocation");
            entity.Property(e => e.Departmentname)
                .HasMaxLength(50)
                .HasColumnName("departmentname");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Doctorid).HasName("doctor_pkey");


            entity.ToTable("doctor");

            entity.Property(e => e.Doctorid).HasColumnName("doctorid");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Apartmentblockno).HasColumnName("apartmentblockno");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .HasColumnName("city");
            entity.Property(e => e.Depid).HasColumnName("depid");
            entity.Property(e => e.Doctorname)
                .HasMaxLength(20)
                .HasColumnName("doctorname");
            entity.Property(e => e.MedicalSpecialization)
                .HasMaxLength(50)
                .HasColumnName("medical_specialization");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying(12)[]")
                .HasColumnName("phone");
            entity.Property(e => e.Salary)
                .HasPrecision(8, 2)
                .HasColumnName("salary");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .HasColumnName("sex");
            entity.Property(e => e.Shift)
                .HasMaxLength(10)
                .HasColumnName("shift");
            entity.Property(e => e.Street)
                .HasMaxLength(20)
                .HasColumnName("street");

            entity.HasOne(d => d.Dep).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.Depid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_doctor");
        });

        modelBuilder.Entity<Doctorwritetestreport>(entity =>
        {
            entity.HasKey(e => new { e.Docid, e.Patid }).HasName("doctorwritetestreport_pkey");

            entity.ToTable("doctorwritetestreport");

            entity.Property(e => e.Docid).HasColumnName("docid");
            entity.Property(e => e.Patid).HasColumnName("patid");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(100)
                .HasColumnName("diagnosis");
            entity.Property(e => e.ReportDate).HasColumnName("reportDate");

            entity.HasOne(d => d.Doc).WithMany(p => p.Doctorwritetestreports)
                .HasForeignKey(d => d.Docid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_doctor_test");

            entity.HasOne(d => d.Pat).WithMany(p => p.Doctorwritetestreports)
                .HasForeignKey(d => d.Patid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_patient_test");
        });

        modelBuilder.Entity<Medicalrecord>(entity =>
        {
            entity.HasKey(e => e.Recordid).HasName("medicalrecord_pkey");


            entity.ToTable("medicalrecord");

            entity.Property(e => e.Recordid).HasColumnName("recordid");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(100)
                .HasColumnName("diagnosis");
            entity.Property(e => e.Patid).HasColumnName("patid");
            entity.Property(e => e.Recorddate).HasColumnName("recorddate");

            entity.HasOne(d => d.Pat).WithMany(p => p.Medicalrecords)
                .HasForeignKey(d => d.Patid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_record");
        });

        modelBuilder.Entity<Nurse>(entity =>
        {
            entity.HasKey(e => e.Nurseid).HasName("nurse_pkey");

            entity.ToTable("nurse");

            entity.Property(e => e.Nurseid).HasColumnName("nurseid");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Apartmentblockno).HasColumnName("apartmentblockno");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .HasColumnName("city");
            entity.Property(e => e.Nursename)
                .HasMaxLength(20)
                .HasColumnName("nursename");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying(13)[]")
                .HasColumnName("phone");
            entity.Property(e => e.Salary)
                .HasPrecision(8, 2)
                .HasColumnName("salary");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .HasColumnName("sex");
            entity.Property(e => e.Shift)
                .HasMaxLength(10)
                .HasColumnName("shift");
            entity.Property(e => e.Street)
                .HasMaxLength(20)
                .HasColumnName("street");
            entity.Property(e => e.SupervisorId).HasColumnName("supervisor_id");
            entity.Property(e => e.Wardid).HasColumnName("wardid");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.InverseSupervisor)
                .HasForeignKey(d => d.SupervisorId)
                .HasConstraintName("fk_supervisor");

            entity.HasOne(d => d.Ward).WithMany(p => p.Nurses)
                .HasForeignKey(d => d.Wardid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ward");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Patientid).HasName("patient_pkey");
            entity.ToTable("patient");

            entity.Property(e => e.Patientid).HasColumnName("patientid");
            entity.Property(e => e.Age)
                .HasPrecision(5)
                .HasColumnName("age");
            entity.Property(e => e.Apartmentblockno).HasColumnName("apartmentblockno");
            entity.Property(e => e.City)
                .HasMaxLength(20)
                .HasColumnName("city");
            entity.Property(e => e.Enterdate).HasColumnName("enterdate");
            entity.Property(e => e.Exitdate).HasColumnName("exitdate");
            entity.Property(e => e.Patientname)
                .HasMaxLength(20)
                .HasColumnName("patientname");
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .HasColumnName("phone");
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .HasColumnName("sex");
            entity.Property(e => e.Street)
                .HasMaxLength(20)
                .HasColumnName("street");
            entity.Property(e => e.Wardid)
                .ValueGeneratedOnAdd()
                .HasColumnName("wardid");

            entity.HasOne(d => d.Ward).WithMany(p => p.Patients)
                .HasForeignKey(d => d.Wardid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_patient");
        });

        modelBuilder.Entity<Recordtreatment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("recordtreatment");

            entity.Property(e => e.Recid).HasColumnName("recid");
            entity.Property(e => e.Treatment)
                .HasMaxLength(50)
                .HasColumnName("treatment");

            entity.HasOne(d => d.Rec).WithMany()
                .HasForeignKey(d => d.Recid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_treatment");
        });

        modelBuilder.Entity<Testreporttreatment>(entity =>
        {
            entity.HasKey(e => new { e.Docid, e.Patid }).HasName("testreporttreatments_pkey");

            entity.ToTable("testreporttreatments");

            entity.Property(e => e.Docid).HasColumnName("docid");
            entity.Property(e => e.Patid).HasColumnName("patid");
            entity.Property(e => e.Treatments)
                .HasMaxLength(50)
                .HasColumnName("treatments");

            entity.HasOne(d => d.Doctorwritetestreport).WithOne(p => p.Testreporttreatment)
                .HasForeignKey<Testreporttreatment>(d => new { d.Docid, d.Patid })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_doctor__test");
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.Wardid).HasName("ward_pkey");

            entity.ToTable("ward");

            entity.Property(e => e.Wardid).HasColumnName("wardid");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Depatmentno).HasColumnName("depatmentno");

            entity.HasOne(d => d.DepatmentnoNavigation).WithMany(p => p.Wards)
                .HasForeignKey(d => d.Depatmentno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ward");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
