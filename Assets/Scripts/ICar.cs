public interface ICar
{
    IDrive Drive { get; set; }

    IEngine Engine { get; set; }

    IWheel FrontWheel { get; }

    IWheel RearWheel { get; }

    void SetFrontSuspensionFrequency(float frequency);

    void SetFrontDampingRatio(float dampingRatio);

    void SetRearSuspensionFrequency(float frequency);

    void SetRearDampingRatio(float dampingRatio);

    void SetFrontSuspensionHeight(float height);

    void SetRearSuspensionHeight(float height);
}