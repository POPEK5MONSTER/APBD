using System.Transactions;
using apbd10.Context;
using apbd10.Models;
using apbd10.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apbd10;


[ApiController]
public class Controller : ControllerBase
{
    public IDbService DbService;
    public HospitalContex Contex;
    
    public Controller(IDbService dbService, HospitalContex contex)
    {
        DbService = dbService;
        Contex = contex;
    }

    [HttpPost]
    [Route("prescription")]
    public async Task<IActionResult> AddPrescription(PrescriptionDTO prescriptionDto)
    {
        var patient = await DbService.DoesPatientExist(prescriptionDto.Patient.IdPatient);

        var exist = await DbService.DoesMedicExist(prescriptionDto.Medicaments);
        if (exist == 1)
        {
            NotFound("lek nie istnieje");
        }

        if (prescriptionDto.Medicaments.Count > 10)
        {
            return NotFound("Recepta ma wiecej niz 10 lekowt");
        }

        if (prescriptionDto.DueDate < prescriptionDto.Date)
        {
            return NotFound("nieporawna data");
        }
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var x = Contex.Prescriptions.Add(new Prescription()
                {
                    Date = prescriptionDto.Date,
                    DueDate = prescriptionDto.DueDate,
                    IdDoctor = 1, 
                    IdPatient = prescriptionDto.Patient.IdPatient
                }
            );
            await Contex.SaveChangesAsync();

            foreach (var medicament in prescriptionDto.Medicaments)
            {
                Contex.PrescriptionMedicaments.Add(
                    new Prescription_Medicament()
                    {
                        IdMedicament = medicament.IdMedicament,
                        IdPrescription = x.Entity.IdPrescription,
                        Details = medicament.Description,
                        Dose = medicament.Dose
                    }
                );
            }

            if (patient == null)
            {
                Contex.Patients.Add(
                    new Patient()
                    {
                        birthday =  prescriptionDto.Patient.birthday,
                        FirstName = prescriptionDto.Patient.FirstName,
                        LastName = prescriptionDto.Patient.LastName,
                        IdPatient = prescriptionDto.Patient.IdPatient
                    }
                );
            }

            await Contex.SaveChangesAsync();
            scope.Complete();
        }

        return Ok("Dodano");
    }

    
    
    [HttpGet]
    [Route("patient/{id:int}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        var patient = await Contex.Patients.Include(x => x.Prescriptions)
            .ThenInclude(x => x.PrescriptionMedicaments).ThenInclude(x => x.IdMedicamentNavigation)
            .FirstOrDefaultAsync(x => x.IdPatient == id);

        if (patient is null)
        {
            return NotFound("nie istnieje taki pacjent");
        }

        var patientInfo = new
        {
            patient.IdPatient,
            patient.FirstName,
            patient.LastName,
            patient.birthday,
            Prescriptions = patient.Prescriptions.Select(x => new
            {
                x.IdPrescription,
                x.Date,
                x.DueDate,
                Prescription_Medicament= x.PrescriptionMedicaments.Select(y => new
                {
                    y.IdMedicament,
                    y.Dose,
                    y.Details
                }).ToList()
            }).ToList()
        };

        return Ok(patientInfo);
    }
}

public class NewPatient
{
    public int IdPatient { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime birthday { get; set; }
}
public class PrescriptionDTO
{
    public NewPatient Patient { get; set; } = null!;

    public List<MedicamentDTO> Medicaments { get; set; } = new List<MedicamentDTO>();

    public DateTime Date { get; set; }

    public DateTime DueDate { get; set; }
}
