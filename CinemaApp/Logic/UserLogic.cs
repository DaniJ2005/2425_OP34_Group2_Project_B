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
    public static UserModel? CurrentUser { get; private set; }

    public UserLogic()
    {
        // Could do something here

    }

    public UserModel CheckLogin(string email, string password)
    {

        UserModel user = UserAccess.GetByEmail(email);
        if (user != null && user.Password == password)
        {
            CurrentUser = user;
            return user;
        }
        return null;
    }
    
    public UserModel RegisterUser(string email, string password, string userName)
    {
        if (IsValidEmail(email) && IsValidPassword(password) && !string.IsNullOrWhiteSpace(userName))
        {
            UserModel user = new UserModel
            {
                Email = email,
                Password = password,
                UserName = userName,
            };
            UserAccess.Write(user);

            return user;
        }

        Console.WriteLine("Could not register user because:");

        if (!IsValidEmail(email))
            Console.WriteLine("- The email address is invalid.");

        if (!IsValidPassword(password))
            Console.WriteLine("- The password must be at least 8 characters long.");

        if (string.IsNullOrWhiteSpace(userName))
            Console.WriteLine("- The full name cannot be empty.");

        return null;
    }

    private bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }

    private bool IsValidPassword(string password)
    {
        return password.Length >= 8;
    }
}




