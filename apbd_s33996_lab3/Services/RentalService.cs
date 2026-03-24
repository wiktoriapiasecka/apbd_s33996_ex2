using apbd_s33996_lab3.Config;
using apbd_s33996_lab3.Enums;
using apbd_s33996_lab3.Exceptions;
using apbd_s33996_lab3.Models;
using apbd_s33996_lab3.Repositories;

namespace apbd_s33996_lab3.Services;

public class RentalService
{
    private readonly RentalRepository _rentalRepository;
    private readonly UserRepository _userRepository;
    private readonly EquipmentRepository _equipmentRepository;

    public RentalService(
        RentalRepository rentalRepository,
        UserRepository userRepository,
        EquipmentRepository equipmentRepository)
    {
        _rentalRepository = rentalRepository;
        _userRepository = userRepository;
        _equipmentRepository = equipmentRepository;
    }

    public Rental RentEquipment(int userId, int equipmentId, int rentalDays)
    {
        var user = _userRepository.GetById(userId)
                   ?? throw new Exception("Nie znaleziono użytkownika.");

        var equipment = _equipmentRepository.GetById(equipmentId)
                        ?? throw new Exception("Nie znaleziono sprzętu.");

        if (equipment.Status != EquipmentStatus.Available)
        {
            throw new EquipmentUnavailableException("Sprzęt nie jest dostępny do wypożyczenia.");
        }

        var activeUserRentals = _rentalRepository.GetActiveRentalsByUserId(userId);

        if (activeUserRentals.Count >= user.MaxActiveRentals)
        {
            throw new RentalLimitExceededException("Użytkownik przekroczył limit aktywnych wypożyczeń.");
        }

        var rental = new Rental(user, equipment, DateTime.Now, rentalDays);

        equipment.Status = EquipmentStatus.Rented;
        _rentalRepository.Add(rental);

        return rental;
    }

    public decimal ReturnEquipment(int rentalId, DateTime returnDate)
    {
        var rental = _rentalRepository.GetById(rentalId)
                     ?? throw new Exception("Nie znaleziono wypożyczenia.");

        if (!rental.IsActive)
        {
            throw new Exception("To wypożyczenie zostało już zakończone.");
        }

        var penalty = CalculatePenalty(rental.DueDate, returnDate);

        rental.ReturnEquipment(returnDate, penalty);
        rental.RentedEquipment.Status = EquipmentStatus.Available;

        return penalty;
    }

    public List<Rental> GetActiveRentalsByUserId(int userId)
    {
        return _rentalRepository.GetActiveRentalsByUserId(userId);
    }

    public List<Rental> GetOverdueRentals()
    {
        return _rentalRepository.GetOverdueRentals();
    }

    public List<Rental> GetAllRentals()
    {
        return _rentalRepository.GetAll();
    }

    private decimal CalculatePenalty(DateTime dueDate, DateTime returnDate)
    {
        if (returnDate.Date <= dueDate.Date)
        {
            return 0m;
        }

        var lateDays = (returnDate.Date - dueDate.Date).Days;
        return lateDays * RentalRules.PenaltyPerDay;
    }
}