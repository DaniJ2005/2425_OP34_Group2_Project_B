public static class UserAdminLogic
{
    public static List<User> GetAllUsers()
    {
        return UserAccess.GetAllUsers();
    }

    public static User GetUserById(int id)
    {
        return UserAccess.GetAllUsers().FirstOrDefault(u => u.Id == id);
    }

    public static bool AddUser(User user)
    {
        if (!UserLogic.ValidateEmail(user.Email))
        {
            LoggerLogic.Instance.Log($"Invalid email: {user.Email}");
            return false;
        }

        if (!UserLogic.ValidatePassword(user.Password))
        {
            LoggerLogic.Instance.Log("Password too short");
            return false;
        }

        if (!UserLogic.ValidateUserName(user.UserName))
        {
            LoggerLogic.Instance.Log($"Invalid username: {user.UserName}");
            return false;
        }

        try
        {
            UserAccess.Write(user);
            LoggerLogic.Instance.Log($"User added: {user.Email}");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error adding user: {ex.Message}");
            return false;
        }
    }

    public static bool UpdateUser(User user)
    {
        try
        {
            UserAccess.Update(user);
            LoggerLogic.Instance.Log($"User updated: {user.Id}");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error updating user: {ex.Message}");
            return false;
        }
    }

    public static bool DeleteUser(User user)
    {
        try
        {
            UserAccess.Delete(user);
            LoggerLogic.Instance.Log($"User deleted: {user.Id}");
            return true;
        }
        catch (Exception ex)
        {
            LoggerLogic.Instance.Log($"Error deleting user: {ex.Message}");
            return false;
        }
    }
}
