using System.Collections.Generic;

public class InterpolatedSum
{
    private int size;

    private Queue<float> queue = new Queue<float>();

    public InterpolatedSum(int size)
    {
        this.size = size;
    }

    public float Sum { get; private set; }

    public void Add(float value)
    {
        value /= size;
        Sum += value;
        queue.Enqueue(value);
        if (queue.Count > size)
        {
            Sum -= queue.Dequeue();
        }
    }

}