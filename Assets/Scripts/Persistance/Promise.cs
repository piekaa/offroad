using System.Threading;

public class Promise
{
    private Thread thread;

    public Promise(Thread thread)
    {
        this.thread = thread;
    }

    public void Wait()
    {
        thread.Join();
    }
}