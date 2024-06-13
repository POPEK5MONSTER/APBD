using apbd10.Context;
using apbd10.Models;

namespace apbd10.Services;

public class DbService : IDbService
{
    public HospitalContex Contex;

    public DbService(HospitalContex hospitalContex)
    {
        Contex = hospitalContex;
    }

    public async Task<Patient?> DoesPatientExist(int id)
    {
        return await Contex.Patients.FindAsync(id);
    }

    public async Task<int> DoesMedicExist(List<MedicamentDTO> medicaments)
    {
        foreach (var medicament in medicaments)
        {
            if (await Contex.Medicaments.FindAsync(medicament.IdMedicament) is null)
            {
                return 1;
            }
        }

        return 0;
    }
}