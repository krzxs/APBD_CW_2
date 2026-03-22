using APBD_CW_2.Domain.Users;
using APBD_CW_2.Exceptions;

namespace APBD_CW_2.DTOs;

public record UserDto(
    string Type,
    string Id,
    string FirstName,
    string LastName)
{
    public static UserDto From(User u) => u switch
    {
        Student s => new UserDto(u.GetUserType(), s.Id, s.FirstName, s.LastName),
        Employee e => new UserDto(u.GetUserType(), e.Id, e.FirstName, e.LastName),
        _ => throw new UnknownTypeException(u.GetUserType())
    };

    public User ToUser() => Type switch
    {
        "Student" => Student.Restore(Id, FirstName, LastName),
        "Employee" => Employee.Restore(Id, FirstName, LastName),
        _ => throw new UnknownTypeException(Type)
    };
}