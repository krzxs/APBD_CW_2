namespace APBD_CW_2.Domain.Users;

public class Employee : User
{
    public override int RentalLimit => 5;

    public Employee(string firstName, string lastName) : base(firstName, lastName)
    {
    }

    private Employee(string id, string firstName, string lastName) : base(id, firstName, lastName)
    {
    }

    public static Employee Restore(string id, string firstName, string lastName)
    {
        return new Employee(id, firstName, lastName);
    }

    public override string GetUserType()
    {
        return "Employee";
    }
}