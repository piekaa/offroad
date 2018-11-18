public delegate void OnClick();

public interface IPiekaButton
{
    void RegisterOnClick(OnClick onClick);

    void UnregisterOnClick(OnClick onClick);
}