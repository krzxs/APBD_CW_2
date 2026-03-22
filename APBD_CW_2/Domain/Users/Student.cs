namespace APBD_CW_2.Domain.Users;

public class Student : User
{
    public override int RentalLimit => 2;

    public Student(string firstName, string lastName) : base(firstName, lastName)
    {
    }

    private Student(string id, string firstName, string lastName) : base(id, firstName, lastName)
    {
    }

    public static Student Restore(string id, string firstName, string lastName)
    {
        return new Student(id, firstName, lastName);
    }

    public override string GetUserType()
    {
        return "Student";
    }
}