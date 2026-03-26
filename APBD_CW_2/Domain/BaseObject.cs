namespace APBD_CW_2.Domain;

public abstract class BaseObject
{
    public string Id { get; }

    protected BaseObject()
    {
        Id = Guid.NewGuid().ToString();
    }

    protected BaseObject(string id)
    {
        Id = id;
    }
}