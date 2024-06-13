using apbd10.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd10.Context;

public class HospitalContex : DbContext
{
    protected HospitalContex(){}

    public HospitalContex(DbContextOptions options) : base(options){}
    
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor(){IdDoctor = 1, FirstName = "Bartek", LastName = "Palmix", Email = "treshmain@wp.pl"},
            new Doctor(){IdDoctor = 2, FirstName = "Krzysiek", LastName = "Pieta", Email = "nightcore@onet.pl"},
            new Doctor(){IdDoctor = 3, FirstName = "Szymon", LastName = "Bielawsksi", Email = "andrzej@o2.pl"}
        );
        modelBuilder.Entity<Medicament>().HasData(
            new Medicament(){IdMedicament = 1, Name = "ibuprom", Description = "na gardlo", Type = "1"},
            new Medicament(){IdMedicament = 2, Name = "apap", Description = "na bol glowy", Type = "2"},
            new Medicament(){IdMedicament = 3, Name = "d3", Description = "na kosci", Type = "3"}
        );
        modelBuilder.Entity<Patient>().HasData(
            new Patient(){IdPatient = 1, FirstName = "Mateusz", LastName = "Popowski", birthday = new DateTime(2003, 9, 21)},
            new Patient(){IdPatient = 2, FirstName = "Bartlomiej", LastName = "Szolkowki", birthday = new DateTime(2001, 1, 1),},
            new Patient(){IdPatient = 3, FirstName = "Grzesiek", LastName = "Rybak", birthday = new DateTime(2002, 1, 1),}
        );
        modelBuilder.Entity<Prescription>().HasData(
            new Prescription(){IdPrescription = 1, Date = new DateTime(2010, 1, 1), DueDate = new DateTime(2010, 1, 2), IdPatient = 2, IdDoctor = 1},
            new Prescription(){IdPrescription = 2, Date = new DateTime(2011, 1, 1), DueDate = new DateTime(2011, 1, 2), IdPatient = 1, IdDoctor = 2},
            new Prescription(){IdPrescription = 3, Date = new DateTime(2012, 1, 1), DueDate = new DateTime(2012, 1, 2), IdPatient = 3, IdDoctor = 3}
        );
        modelBuilder.Entity<Prescription_Medicament>().HasData(
            new Prescription_Medicament(){IdPrescription = 2, IdMedicament = 2,  Dose = 1, Details = "detal1"},
            new Prescription_Medicament(){IdPrescription = 1, IdMedicament = 1, Dose = 2, Details = "detal2"},
            new Prescription_Medicament(){IdPrescription = 3, IdMedicament = 3,  Dose = 4, Details = "detal3"}
        );
        
        base.OnModelCreating(modelBuilder);
    }
}