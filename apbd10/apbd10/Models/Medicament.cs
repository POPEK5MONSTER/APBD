using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd10.Models;

[Table("Medicament")]
public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }

    [MaxLength(100)]
    [Required]
    public string Name { get; set; } = null!;
    
    [MaxLength(100)]
    [Required]
    public string Description { get; set; } = null!;
    
    [MaxLength(100)]
    [Required]
    public string Type { get; set; } = null!;

    private List<Prescription_Medicament> PrescriptionMedicaments { get; set; } = new List<Prescription_Medicament>();    
}