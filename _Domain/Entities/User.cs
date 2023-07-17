using Domain.Bases;

namespace Domain.Entities;

public class User : BaseEntity
{
    public User()
    {
    }

    public User(string firstName, string lastName, string email, byte[] password, byte[] salt) : this()
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Salt = salt;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public byte[] Password { get; set; }
    public byte[] Salt { get; set; }
}