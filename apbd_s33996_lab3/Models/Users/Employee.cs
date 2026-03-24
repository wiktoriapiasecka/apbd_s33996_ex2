namespace apbd_s33996_lab3.Models.Users;

public class Employee : User
{
    public string Department { get; set; }
    public string Position { get; set; }

    public Employee(string firstName, string lastName, string department, string position)
        : base(firstName, lastName)
    {
        Department = department;
        Position = position;
    }

    public override int MaxActiveRentals => 5;
    public override string UserType => "Pracownik";

    public override string ToString()
    {
        return base.ToString() + $", Dział: {Department}, Stanowisko: {Position}";
    }
}