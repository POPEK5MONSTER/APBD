using apbd10.Models;

namespace apbd10.Services;

public interface IDbService
{
    Task<Patient?> DoesPatientExist(int id);

    Task<int> DoesMedicExist(List<MedicamentDTO> medicaments);
}