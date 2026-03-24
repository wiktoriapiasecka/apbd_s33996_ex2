using EquipmentEntity = apbd_s33996_lab3.Models.Equipment.Equipment;
using apbd_s33996_lab3.Models.Users;

namespace apbd_s33996_lab3.Models;

public class Rental
{
    private static int _nextId = 1;

    public int Id { get; }
    public User User { get; }
    public EquipmentEntity RentedEquipment { get; }
    public DateTime RentalDate { get; }
    public int RentalDays { get; }
    public DateTime DueDate => RentalDate.AddDays(RentalDays);
    public DateTime? ReturnDate { get; private set; }
    public decimal Penalty { get; private set; }

    public bool IsActive => ReturnDate == null;
    public bool IsReturnedOnTime => ReturnDate != null && ReturnDate.Value.Date <= DueDate.Date;
    public bool IsOverdue => IsActive && DateTime.Now.Date > DueDate.Date;

    public Rental(User user, EquipmentEntity rentedEquipment, DateTime rentalDate, int rentalDays)
    {
        Id = _nextId++;
        User = user;
        RentedEquipment = rentedEquipment;
        RentalDate = rentalDate;
        RentalDays = rentalDays;
    }

    public void ReturnEquipment(DateTime returnDate, decimal penalty)
    {
        ReturnDate = returnDate;
        Penalty = penalty;
    }

    public override string ToString()
    {
        var status = IsActive ? "Aktywne" : "Zakończone";
        return $"Wypożyczenie ID: {Id}, Użytkownik: {User.FirstName} {User.LastName}, " +
               $"Sprzęt: {RentedEquipment.Name}, Data wypożyczenia: {RentalDate:yyyy-MM-dd}, " +
               $"Termin zwrotu: {DueDate:yyyy-MM-dd}, Status: {status}, Kara: {Penalty} zł";
    }
}