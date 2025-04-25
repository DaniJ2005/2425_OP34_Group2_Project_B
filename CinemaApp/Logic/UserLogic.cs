using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class UserLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in user from anywhere in the program
    //private set, so this can only be set by the class itself
    public static User? CurrentUser { get; private set; }

    public UserLogic()
    {
        // Could do something here
    }

    public User CheckLogin(string email, string password)
    {

        User user = UserAccess.GetByEmail(email);
        if (user != null && user.Password == password)
        {
            CurrentUser = user;
            LoggerLogic.Instance.Log($"User logged in | ID: {CurrentUser.Id} | Email: {CurrentUser.Email} | Role: {CurrentUser.RoleId}");

            return user;
        }
        LoggerLogic.Instance.Log($"User login failed | Email: {email}");
        return null;
    }
    
    public User RegisterUser(string email, string password, string userName)
    {
        if (IsValidEmail(email) && IsValidPassword(password) && !string.IsNullOrWhiteSpace(userName))
        {
            User user = new User
            {
                Email = email,
                Password = password,
                UserName = userName,
            };
            UserAccess.Write(user);
            LoggerLogic.Instance.Log($"User registerd | ID: {email} | UserName: {userName}");

            return user;
        }

        Console.WriteLine("Could not register user because:");

        string message = "";

        if (!IsValidEmail(email))
            message += "- The email address is invalid.\n";

        if (!IsValidPassword(password))
            message += "- The password must be at least 8 characters long.\n";

        if (string.IsNullOrWhiteSpace(userName))
            message += "- The full name cannot be empty.\n";

        if (string.IsNullOrWhiteSpace(message))
            message = "- Unknown error.";

        LoggerLogic.Instance.Log("Registration failure | reasons:\n" + message.TrimEnd());
        Console.WriteLine(message);


        return null;
    }

    public static bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }

    private bool IsValidPassword(string password)
    {
        return password.Length >= 8;
    }
}
