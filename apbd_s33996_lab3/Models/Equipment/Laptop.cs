namespace apbd_s33996_lab3.Models.Equipment;

public class Laptop : Equipment
{
    public string Processor { get; set; }
    public int RamGb { get; set; }

    public Laptop(string name, string processor, int ramGb) : base(name)
    {
        Processor = processor;
        RamGb = ramGb;
    }

    public override string ToString()
    {
        return base.ToString() + $", Procesor: {Processor}, RAM: {RamGb} GB";
    }
}