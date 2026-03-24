using apbd_s33996_lab3.Enums;
using apbd_s33996_lab3.Repositories;

namespace apbd_s33996_lab3.Services;

public class ReportService
{
    private readonly EquipmentRepository _equipmentRepository;
    private readonly RentalRepository _rentalRepository;

    public ReportService(EquipmentRepository equipmentRepository, RentalRepository rentalRepository)
    {
        _equipmentRepository = equipmentRepository;
        _rentalRepository = rentalRepository;
    }

    public void PrintSummary()
    {
        var equipment = _equipmentRepository.GetAll();
        var rentals = _rentalRepository.GetAll();

        Console.WriteLine("===== RAPORT KOŃCOWY =====");
        Console.WriteLine($"Liczba wszystkich sprzętów: {equipment.Count}");
        Console.WriteLine($"Dostępne: {equipment.Count(e => e.Status == EquipmentStatus.Available)}");
        Console.WriteLine($"Wypożyczone: {equipment.Count(e => e.Status == EquipmentStatus.Rented)}");
        Console.WriteLine($"Niedostępne: {equipment.Count(e => e.Status == EquipmentStatus.Unavailable)}");
        Console.WriteLine($"Liczba wszystkich wypożyczeń: {rentals.Count}");
        Console.WriteLine($"Aktywne wypożyczenia: {rentals.Count(r => r.IsActive)}");
        Console.WriteLine($"Przeterminowane wypożyczenia: {rentals.Count(r => r.IsOverdue)}");
        Console.WriteLine($"Suma naliczonych kar: {rentals.Sum(r => r.Penalty)} zł");
    }
}