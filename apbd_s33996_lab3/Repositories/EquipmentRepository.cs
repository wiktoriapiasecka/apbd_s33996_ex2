using apbd_s33996_lab3.Models.Equipment;

namespace apbd_s33996_lab3.Repositories;

public class EquipmentRepository
{
    private readonly List<Equipment> _equipment = new();

    public void Add(Equipment equipment)
    {
        _equipment.Add(equipment);
    }

    public List<Equipment> GetAll()
    {
        return _equipment;
    }

    public Equipment? GetById(int id)
    {
        return _equipment.FirstOrDefault(e => e.Id == id);
    }

    public List<Equipment> GetAvailable()
    {
        return _equipment.Where(e => e.Status == Enums.EquipmentStatus.Available).ToList();
    }
}