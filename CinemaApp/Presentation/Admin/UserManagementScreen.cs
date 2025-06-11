public class UserManagementScreen : IScreen
{
    public string ScreenName { get; set; } = "User Management";

    public void Start()
    {
        int selectedIndex = 0;
        ConsoleKey key;
        string[] options = { "Add User", "Update User", "Delete User", "View Users", "Back" };

        do
        {
            General.ClearConsole();
            General.PrintColoredBoxedTitle($"{ScreenName}", ConsoleColor.Yellow);
            Console.WriteLine("[↑][↓] to navigate, [ENTER] to select, [ESC] to go back\n");

            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine($"> {options[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {options[i]}");
                }
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && selectedIndex > 0) selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1) selectedIndex++;
            else if (key == ConsoleKey.Enter)
            {
                General.ClearConsole();
                switch (selectedIndex)
                {
                    case 0: ShowAddUser(); break;
                    case 1: ShowUpdateUser(); break;
                    case 2: ShowDeleteUser(); break;
                    case 3: ShowViewUsers(); break;
                    case 4:
                        MenuLogic.NavigateToPrevious();
                        return;
                }
            }
            else if (key == ConsoleKey.Escape)
            {
                MenuLogic.NavigateToPrevious();
                return;
            }

        } while (true);
    }

    private void ShowAddUser()
    {
        var fields = new List<FormField>
        {
            new FormField("Email", false, v => (UserLogic.ValidateEmail(v), UserLogic.ValidateEmail(v) ? "" : "Invalid email")),
            new FormField("UserName", false, v => (UserLogic.ValidateUserName(v), UserLogic.ValidateUserName(v) ? "" : "Invalid username")),
            new FormField("Password", true, v => (UserLogic.ValidatePassword(v), UserLogic.ValidatePassword(v) ? "" : "Invalid password")),
            new FormField("RoleId", false, v => {
                var valid = new[] { "1", "2", "3" }.Contains(v);
                return (valid, valid ? "" : "RoleId must be 1 (Admin), 2 (User), or 3 (Guest)");
            })
        };

        var createScreen = new CreateScreen<User>(
            "Add User",
            fields,
            () => new User
            {
                Email = fields[0].Value,
                UserName = fields[1].Value,
                Password = fields[2].Value,
                RoleId = int.Parse(fields[3].Value)
            },
            UserAdminLogic.AddUser
        );

        createScreen.Start();
    }

    private void ShowUpdateUser()
    {
        var users = UserAdminLogic.GetAllUsers();
        if (users.Count == 0)
        {
            Console.WriteLine("No users to update.");
            Console.ReadKey();
            return;
        }

        var table = new Table<User>(maxColWidth: 40, pageSize: 10);
        table.SetColumns("Id", "Email", "UserName", "Password", "RoleId");
        table.AddRows(users);

        ConsoleKey key;
        do
        {
            General.ClearConsole();
            Console.WriteLine("Select user to update:\n");
            table.Print("Id", "Email", "UserName", "Password", "RoleId");
            Console.WriteLine("\n[↑][↓] Navigate  [←][→] Page  [ENTER] Edit  [ESC] Cancel");

            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Enter)
            {
                var selected = table.GetSelected();

                var fields = new List<FormField>
                {
                    new FormField("Email", false, v => (UserLogic.ValidateEmail(v), UserLogic.ValidateEmail(v) ? "" : "Invalid email"))
                        { Value = selected.Email, OriginalValue = selected.Email },
                    new FormField("UserName", false, v => (UserLogic.ValidateUserName(v), UserLogic.ValidateUserName(v) ? "" : "Invalid username"))
                        { Value = selected.UserName, OriginalValue = selected.UserName },
                    new FormField("Password", true, v => (UserLogic.ValidatePassword(v), UserLogic.ValidatePassword(v) ? "" : "Invalid password"))
                        { Value = selected.Password, OriginalValue = selected.Password },
                    new FormField("RoleId", false, v => {
                        var valid = new[] { "1", "2", "3" }.Contains(v);
                        return (valid, valid ? "" : "RoleId must be 1 (Admin), 2 (User), or 3 (Guest)");
                    })
                        { Value = selected.RoleId.ToString(), OriginalValue = selected.RoleId.ToString() }
                };

                var updateScreen = new UpdateScreen<User>(
                    "Update User",
                    fields,
                    () => new User
                    {
                        Id = selected.Id,
                        Email = fields[0].Value,
                        UserName = fields[1].Value,
                        Password = fields[2].Value,
                        RoleId = int.Parse(fields[3].Value)
                    },
                    UserAdminLogic.UpdateUser
                );

                MenuLogic.NavigateTo(updateScreen);
                return;
            }
            else if (key == ConsoleKey.UpArrow) table.MoveUp();
            else if (key == ConsoleKey.DownArrow) table.MoveDown();
            else if (key == ConsoleKey.LeftArrow) table.PreviousPage();
            else if (key == ConsoleKey.RightArrow) table.NextPage();
            else if (key == ConsoleKey.Escape) return;

        } while (true);
    }

    private void ShowDeleteUser()
    {
        var deleteScreen = new DeleteScreen<User>(
            UserAdminLogic.GetAllUsers,
            u => UserAdminLogic.DeleteUser(u)
        );

        deleteScreen.Start();
    }

    private void ShowViewUsers()
    {
        var readScreen = new ReadScreen<User>(
            UserAdminLogic.GetAllUsers,
            new[] { "Id", "Email", "UserName", "Password", "RoleId" }
        );

        readScreen.Start();
    }
}
