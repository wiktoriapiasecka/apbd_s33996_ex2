using apbd_s33996_lab3.Enums;
using apbd_s33996_lab3.Exceptions;
using apbd_s33996_lab3.Models.Equipment;
using apbd_s33996_lab3.Models.Users;
using apbd_s33996_lab3.Repositories;
using apbd_s33996_lab3.Services;

var equipmentRepository = new EquipmentRepository();
var userRepository = new UserRepository();
var rentalRepository = new RentalRepository();

var equipmentService = new EquipmentService(equipmentRepository);
var userService = new UserService(userRepository);
var rentalService = new RentalService(rentalRepository, userRepository, equipmentRepository);
var reportService = new ReportService(equipmentRepository, rentalRepository);

Console.WriteLine("======================================");
Console.WriteLine("   SYSTEM UCZELNIANEJ WYPOŻYCZALNI    ");
Console.WriteLine("======================================");
Console.WriteLine();

var laptop1 = new Laptop("Dell Latitude 5540", "Intel i7", 16);
var laptop2 = new Laptop("Lenovo ThinkPad E14", "AMD Ryzen 7", 32);
var projector1 = new Projector("Epson EB-FH52", 4000, "1920x1080");
var camera1 = new Camera("Canon EOS 250D", 24, "18-55mm");
var camera2 = new Camera("Nikon D3500", 24, "70-300mm");

equipmentService.AddEquipment(laptop1);
equipmentService.AddEquipment(laptop2);
equipmentService.AddEquipment(projector1);
equipmentService.AddEquipment(camera1);
equipmentService.AddEquipment(camera2);

Console.WriteLine("Dodano sprzęt:");
foreach (var equipment in equipmentService.GetAllEquipment())
{
    Console.WriteLine(equipment);
}

Console.WriteLine();
var student1 = new Student("Anna", "Kowalska", "s12345", "Informatyka");
var student2 = new Student("Jan", "Nowak", "s54321", "Zarządzanie");
var employee1 = new Employee("Piotr", "Wiśniewski", "IT", "Administrator");

userService.AddUser(student1);
userService.AddUser(student2);
userService.AddUser(employee1);

Console.WriteLine("Dodano użytkowników:");
foreach (var user in userService.GetAllUsers())
{
    Console.WriteLine(user);
}

Console.WriteLine();
Console.WriteLine("Dostępny sprzęt:");
foreach (var equipment in equipmentService.GetAvailableEquipment())
{
    Console.WriteLine(equipment);
}

Console.WriteLine();
Console.WriteLine("===== POPRAWNE WYPOŻYCZENIE =====");
var rental1 = rentalService.RentEquipment(student1.Id, laptop1.Id, 7);
Console.WriteLine("Utworzono wypożyczenie:");
Console.WriteLine(rental1);

Console.WriteLine();
Console.WriteLine("===== PRÓBA NIEPOPRAWNEJ OPERACJI =====");
equipmentService.MarkAsUnavailable(projector1.Id);

try
{
    rentalService.RentEquipment(student2.Id, projector1.Id, 5);
}
catch (EquipmentUnavailableException ex)
{
    Console.WriteLine($"Błąd: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("===== PRÓBA PRZEKROCZENIA LIMITU STUDENTA =====");
try
{
    var rental2 = rentalService.RentEquipment(student1.Id, laptop2.Id, 5);
    Console.WriteLine(rental2);

    var rental3 = rentalService.RentEquipment(student1.Id, camera1.Id, 4);
    Console.WriteLine(rental3);

    var rental4 = rentalService.RentEquipment(student1.Id, camera2.Id, 3);
    Console.WriteLine(rental4);
}
catch (RentalLimitExceededException ex)
{
    Console.WriteLine($"Błąd: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("===== AKTYWNE WYPOŻYCZENIA STUDENTA =====");
foreach (var rental in rentalService.GetActiveRentalsByUserId(student1.Id))
{
    Console.WriteLine(rental);
}

Console.WriteLine();
Console.WriteLine("===== ZWROT W TERMINIE =====");
var penaltyOnTime = rentalService.ReturnEquipment(rental1.Id, rental1.DueDate);
Console.WriteLine($"Zwrot wykonany. Kara: {penaltyOnTime} zł");

Console.WriteLine();
Console.WriteLine("===== WYPOŻYCZENIE I ZWROT PO TERMINIE =====");
var rentalLate = rentalService.RentEquipment(employee1.Id, camera2.Id, 2);
Console.WriteLine("Nowe wypożyczenie:");
Console.WriteLine(rentalLate);

var lateReturnDate = rentalLate.DueDate.AddDays(3);
var latePenalty = rentalService.ReturnEquipment(rentalLate.Id, lateReturnDate);
Console.WriteLine($"Zwrot po terminie. Kara: {latePenalty} zł");

Console.WriteLine();
Console.WriteLine("===== LISTA PRZETERMINOWANYCH WYPOŻYCZEŃ =====");
var overdueRentals = rentalService.GetOverdueRentals();
if (overdueRentals.Count == 0)
{
    Console.WriteLine("Brak aktualnie przeterminowanych wypożyczeń.");
}
else
{
    foreach (var rental in overdueRentals)
    {
        Console.WriteLine(rental);
    }
}

Console.WriteLine();
reportService.PrintSummary();

Console.WriteLine();
Console.WriteLine("===== AKTUALNY STAN SPRZĘTU =====");
foreach (var equipment in equipmentService.GetAllEquipment())
{
    Console.WriteLine(equipment);
}