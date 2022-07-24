namespace Demo.Core.Domain.Users;

public class User : BaseEntity
{
    public UserId Id { get; private init; }
    public UserName Name { get; private set; }

    public string Email { get; private set; }

    public string Password { get; private set; }

    private User()
    {
    }

    public User(UserId id, UserName name, string email, string password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }

    public void ChangeName(UserName userName)
    {
        Name = userName ?? throw new ArgumentNullException(nameof(userName));
        AddDomainEvent(new UserNameChangedEvent(this));
    }

    public static User CreateInstance(UserId userId, UserName name, string email, string password) => new(userId, name, email, password);
    public bool CheckPassword(string password)
    {
        return Password == password;
    }
}