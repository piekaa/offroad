public interface ICar
{
    IDrive Drive { get; set; }

    IEngine Engine { get; set; }

    Wheel FrontWheel { get; }

    Wheel RearWheel { get; }
}