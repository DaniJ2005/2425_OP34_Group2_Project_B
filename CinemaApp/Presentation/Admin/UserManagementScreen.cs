public class UserManagementScreen : IScreen
{
    public string ScreenName { get; set; } = "User Management";
    ConsoleKey key;
    
    public void Start()
    {
        int selectedIndex = 0;
        string[] options = { "Add User", "Update User", "Delete User", "View Users", "Back" };

        do
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║       USER MANAGEMENT        ║");
            Console.WriteLine("╚══════════════════════════════╝");
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

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < options.Length - 1)
                selectedIndex++;
            else if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                switch (selectedIndex)
                {
                    case 0:
                        AddUserFlow();
                        break;
                    case 1:
                        UpdateUserFlow();
                        break;
                    case 2:
                        DeleteUserFlow();
                        break;
                    case 3:
                        ViewUsersFlow();
                        break;
                    case 4:
                        MenuLogic.NavigateToPrevious();
                        LoggerLogic.Instance.Log("Returned to admin menu from user management");
                        return;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            else if (key == ConsoleKey.Escape)
            {
                MenuLogic.NavigateToPrevious();
                LoggerLogic.Instance.Log("User pressed Escape - returning to admin menu");
                return;
            }
        } while (true);
    }

    private void AddUserFlow()
    {
        Console.WriteLine("=== Add New User ===\n");

        string email = GetValidInput("Email", s => UserLogic.ValidateEmail(s), "Please enter a valid email address");
        string username = GetValidInput("Username", s => UserLogic.ValidateUserName(s), "Username must be at least 3 characters");
        string password = GetSecurePassword();

        // Default role ID (you might want to add role selection)
        int roleId = 2;

        var user = new User 
        { 
            Email = email, 
            UserName = username, 
            Password = password,
            RoleId = roleId
        };

        if (UserAdminLogic.AddUser(user))
            Console.WriteLine("\nUser successfully added.");
        else
            Console.WriteLine("\nFailed to add user. Please check the logs for details.");
    }

    private void UpdateUserFlow()
    {
        Console.WriteLine("=== Update User ===\n");
        
        // First display users to help with selection
        ViewUsersFlow();
        
        // Get user ID with error handling
        int id;
        User user = null;
        
        do {
            Console.Write("\nEnter user ID to update (or 0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID format. Please enter a number.");
                continue;
            }
            
            if (id == 0) return;
            
            user = UserAdminLogic.GetUserById(id);
            if (user == null)
                Console.WriteLine("User not found. Please try again.");
            
                
        } while (user == null);

        // Now update user properties with current values as defaults
        Console.WriteLine($"\nUpdating user: {user.UserName} (ID: {user.Id})");
        Console.WriteLine("(Press Enter to keep current value)\n");

        string input;
        
        Console.Write($"New Email [{user.Email}]: ");
        input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
        {
            if (UserLogic.ValidateEmail(input))
                user.Email = input;
            else
                Console.WriteLine("Invalid email format - keeping current email.");
        }

        Console.Write($"New Username [{user.UserName}]: ");
        input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
        {
            if (UserLogic.ValidateUserName(input))
                user.UserName = input;
            else
                Console.WriteLine("Invalid username format - keeping current username.");
        }

        Console.Write("New Password (leave blank to keep current): ");
        input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
        {
            if (UserLogic.ValidatePassword(input))
                user.Password = input;
            else
                Console.WriteLine("Invalid password - keeping current password.");
        }

        if (UserAdminLogic.UpdateUser(user))
            Console.WriteLine("\nUser updated successfully.");
        else
            Console.WriteLine("\nFailed to update user. Please check the logs for details.");
    }

    private void DeleteUserFlow()
    {
        Console.WriteLine("=== Delete User ===\n");
        
        // First display users to help with selection
        ViewUsersFlow();
        
        int id;
        User user = null;
        
        do {
            Console.Write("\nEnter user ID to delete (or 0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID format. Please enter a number.");
                continue;
            }
            
            if (id == 0) return;
            
            user = UserAdminLogic.GetUserById(id);
            if (user == null)
                Console.WriteLine("User not found. Please try again.");
                
        } while (user == null);

        Console.WriteLine($"\nAre you sure you want to delete user: {user.UserName} (ID: {user.Id})? [y/N]");
        string confirmation = Console.ReadLine().ToLower();
        
        if (confirmation == "y" || confirmation == "yes")
        {
            if (UserAdminLogic.DeleteUser(user))
                Console.WriteLine("User deleted successfully.");
            else
                Console.WriteLine("Failed to delete user. Please check the logs for details.");
        }
        else
        {
            Console.WriteLine("Delete operation cancelled.");
        }
    }

    private void ViewUsersFlow()
    {
        var users = UserAdminLogic.GetAllUsers();
        
        if (users.Count == 0)
        {
            Console.WriteLine("No users found in the system.");
            return;
        }
        
        Console.WriteLine("╔════╦══════════════════════════╦═══════════════╦════════╗");
        Console.WriteLine("║ ID ║ Email                    ║ Username      ║ Role   ║");
        Console.WriteLine("╠════╬══════════════════════════╬═══════════════╬════════╣");

        foreach (var user in users)
        {
            string roleName = GetRoleName(user.RoleId);
            Console.WriteLine($"║ {user.Id,-2} ║ {user.Email,-24} ║ {user.UserName,-13} ║ {roleName,-6} ║");
        }

        Console.WriteLine("╚════╩══════════════════════════╩═══════════════╩════════╝");
    }
    
    // Helper methods for input validation
    private string GetValidInput(string prompt, Func<string, bool> validator, string errorMessage)
    {
        string input;
        bool isValid;
        
        do {
            Console.Write($"{prompt}: ");
            input = Console.ReadLine();
            isValid = validator(input);
            
            if (!isValid)
                Console.WriteLine(errorMessage);
                
        } while (!isValid);
        
        return input;
    }
    
    private string GetSecurePassword()
    {
        string password;
        bool isValid;
        
        do {
            Console.Write("Password: ");
            password = ReadMaskedInput();
            isValid = UserLogic.ValidatePassword(password);
            
            if (!isValid)
                Console.WriteLine("Password must be at least 8 characters with uppercase, lowercase, and numbers");
                
        } while (!isValid);
        
        return password;
    }
    
    private string ReadMaskedInput()
    {
        string input = "";
        ConsoleKeyInfo key;
        
        do {
            key = Console.ReadKey(true);
            
            if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace)
            {
                input += key.KeyChar;
                Console.Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input = input.Substring(0, input.Length - 1);
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter);
        
        Console.WriteLine();
        return input;
    }
    
    private string GetRoleName(int roleId)
    {
        // This would ideally come from a role service or database
        switch (roleId)
        {
            case 1: return "Admin";
            case 2: return "User";
            case 3: return "Guest";
            default: return "Unknown";
        }
    }
}