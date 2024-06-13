using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace apbd10.Models;

[Table("Prescription_Medicament")]
[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]  
public class Prescription_Medicament
{
    public int IdMedicament { get; set; }

    public int IdPrescription { get; set; }
    
    public int Dose { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Details { get; set; } = null!;

    [ForeignKey(nameof(IdMedicament))] public Medicament IdMedicamentNavigation { get; set; } = null!;

    [ForeignKey(nameof(IdPrescription))] public Prescription IdPrescriptionNavigation { get; set; } = null!;
}