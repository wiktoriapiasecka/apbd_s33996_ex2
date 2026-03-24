namespace apbd_s33996_lab3.Models.Equipment;

public class Camera : Equipment
{
    public int Megapixels { get; set; }
    public string LensType { get; set; }

    public Camera(string name, int megapixels, string lensType) : base(name)
    {
        Megapixels = megapixels;
        LensType = lensType;
    }

    public override string ToString()
    {
        return base.ToString() + $", Mpix: {Megapixels}, Obiektyw: {LensType}";
    }
}