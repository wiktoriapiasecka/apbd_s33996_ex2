using apbd_s33996_lab3.Enums;

namespace apbd_s33996_lab3.Models.Equipment;

public abstract class Equipment
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Name { get; set; }
    public EquipmentStatus Status { get; set; }

    protected Equipment(string name)
    {
        Id = _nextId++;
        Name = name;
        Status = EquipmentStatus.Available;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Nazwa: {Name}, Status: {Status}";
    }
}