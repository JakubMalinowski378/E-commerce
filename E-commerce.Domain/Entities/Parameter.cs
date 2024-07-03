namespace E_commerce.Domain.Entities;
public abstract class Parameter
{
    public Guid Id { get; set; }
}
public class MockupParameter : Parameter
{
    public string Condition { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public double Width { get; set; }   
    public double Height { get; set; }
    public double Weight { get; set; }

}
public class ElectronicsParameter: Parameter
{
    public string Condition { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public double Width {  get; set; }
    public double Height { get; set; }
}
public class ClothesParameter : Parameter
{
    public string Condition { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Size {  get; set; } = string.Empty;
    public char Gender { get; set; }
}
public class EntertainmentParameter : Parameter
{
    public string Condition { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Author {  get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Language {  get; set; } = string.Empty;
}
public class MotoringParameter : Parameter
{
    public string Condition { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public double Width { get; set; }
    public double Height { get; set; }
    public string BodyType { get; set; } = string.Empty;
    public string Power { get; set; } = string.Empty;
}
public class HousesPararameter : Parameter
{
    public string Condition { get; set; } = string.Empty;
    public double Area {  get; set; }
    public string Type { get; set; } = string.Empty;
}
