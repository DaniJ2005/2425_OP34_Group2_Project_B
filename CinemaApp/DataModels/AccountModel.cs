public class AccountModel
{
    private static Int64 _nextId = 1;
    public Int64 Id { get; private set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }

    // Constructor with manual ID (optional)
    public AccountModel(Int64 id, string email, string password, string fullname)
    {
        Id = id;
        EmailAddress = email;
        Password = password;
        FullName = fullname;
    }

    // Overloaded constructor with auto-incremented ID
    public AccountModel(string email, string password, string fullname)
    {
        Id = _nextId++;
        EmailAddress = email;
        Password = password;
        FullName = fullname;
    }
}


