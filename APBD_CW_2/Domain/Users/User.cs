namespace APBD_CW_2.Domain.Users;

public abstract class User
{
    public string Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public abstract int RentalLimit { get; }

    protected User(string firstName, string lastName)
    {
        Id = Guid.NewGuid().ToString();
        FirstName = firstName;
        LastName = lastName;
    }

    protected User(string id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public abstract string GetUserType();

    public override string ToString()
    {
        return $"({Id}) {GetUserType()}: {FirstName} {LastName}";
    }
}