namespace apbd_s33996_lab3.Models.Equipment;

public class Projector : Equipment
{
    public int BrightnessLumens { get; set; }
    public string Resolution { get; set; }

    public Projector(string name, int brightnessLumens, string resolution) : base(name)
    {
        BrightnessLumens = brightnessLumens;
        Resolution = resolution;
    }

    public override string ToString()
    {
        return base.ToString() + $", Jasność: {BrightnessLumens} lm, Rozdzielczość: {Resolution}";
    }
}