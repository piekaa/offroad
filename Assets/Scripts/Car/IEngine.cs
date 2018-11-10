namespace Pieka.Car
{
    interface IEngine
    {
        /// <value>0-1</value>
        float Throttle { get; set; }

        IDrive Drive { get; set; }
    }
}