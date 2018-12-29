using UnityEngine;

public class DtoWithTransform
{
    public Vector3Dto Position;
    public Vector3Dto Rotation;
    public Vector3Dto Scale;
    public string Name;
    
    protected void SetTransform(Transform transform)
    {
        Position = new Vector3Dto(transform.position);
        Rotation = new Vector3Dto(transform.rotation);
        Scale = new Vector3Dto(transform.localScale);
        Name = transform.name;
    }

    public DtoWithTransform()
    {
    }

    public DtoWithTransform(Transform transform)
    {
        Position = new Vector3Dto(transform.position);
        Rotation = new Vector3Dto(transform.rotation);
        Scale = new Vector3Dto(transform.localScale);
        Name = transform.name;
    }


    public override string ToString()
    {
        return string.Format("Position: {0}, Rotation: {1}, Scale: {2}", Position, Rotation, Scale);
    }
}