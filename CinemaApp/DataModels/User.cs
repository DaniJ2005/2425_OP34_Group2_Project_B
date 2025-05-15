public class User
{
    public int Id { get; set; }
    private int? _roleId;
    public int RoleId
    {
        get => _roleId ?? 0;
        set => _roleId = value;
    }
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }

}