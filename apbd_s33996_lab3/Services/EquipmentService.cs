using apbd_s33996_lab3.Enums;
using apbd_s33996_lab3.Models.Equipment;
using apbd_s33996_lab3.Repositories;

namespace apbd_s33996_lab3.Services;

public class EquipmentService
{
    private readonly EquipmentRepository _equipmentRepository;

    public EquipmentService(EquipmentRepository equipmentRepository)
    {
        _equipmentRepository = equipmentRepository;
    }

    public void AddEquipment(Equipment equipment)
    {
        _equipmentRepository.Add(equipment);
    }

    public List<Equipment> GetAllEquipment()
    {
        return _equipmentRepository.GetAll();
    }

    public List<Equipment> GetAvailableEquipment()
    {
        return _equipmentRepository.GetAvailable();
    }

    public Equipment? GetEquipmentById(int id)
    {
        return _equipmentRepository.GetById(id);
    }

    public void MarkAsUnavailable(int equipmentId)
    {
        var equipment = _equipmentRepository.GetById(equipmentId);

        if (equipment != null)
        {
            equipment.Status = EquipmentStatus.Unavailable;
        }
    }
}