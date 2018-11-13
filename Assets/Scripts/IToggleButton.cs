/// <returns>state after toggle</returns>
public delegate bool OnToggle();

public interface IToggleButton
{
    void SetOnToggle(OnToggle onToggle);
    /// <summary>
    /// True - on
    /// False - off
    /// </summary>
    /// <param name="state"></param>
    void SetInitialState(bool state);
}