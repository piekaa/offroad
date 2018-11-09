public interface IEngine
{
    //0-1
    float Throttle { get; set; }

    IDrive Drive { get; set; }
}