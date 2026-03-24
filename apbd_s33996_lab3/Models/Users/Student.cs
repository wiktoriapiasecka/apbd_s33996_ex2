namespace apbd_s33996_lab3.Models.Users;

public class Student : User
{
    public string StudentNumber { get; set; }
    public string FieldOfStudy { get; set; }

    public Student(string firstName, string lastName, string studentNumber, string fieldOfStudy)
        : base(firstName, lastName)
    {
        StudentNumber = studentNumber;
        FieldOfStudy = fieldOfStudy;
    }

    public override int MaxActiveRentals => 2;
    public override string UserType => "Student";

    public override string ToString()
    {
        return base.ToString() + $", Nr albumu: {StudentNumber}, Kierunek: {FieldOfStudy}";
    }
}