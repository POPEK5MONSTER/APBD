using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd10.Models;

[Table("Prescription")]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    public int IdPatient { get; set; }

    [Required]
    public int IdDoctor { get; set; }
    
    [ForeignKey(nameof(IdPatient))] public Patient IdPatientNavigation { get; set; } = null!;

    [ForeignKey(nameof(IdDoctor))] public Doctor IdDoctorNavigation { get; set; } = null!;

    
    public List<Prescription_Medicament> PrescriptionMedicaments { get; set; } = new List<Prescription_Medicament>();
}