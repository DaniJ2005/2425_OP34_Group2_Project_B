using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        // Could do something here

    }

    public AccountModel CheckLogin(string email, string password)
    {

        AccountModel acc = AccountsAccess.GetByEmail(email);
        if (acc != null && acc.Password == password)
        {
            CurrentAccount = acc;
            return acc;
        }
        return null;
    }
    
    public AccountModel RegisterAccount(string email, string password, string fullName)
    {
        if (IsValidEmail(email) && IsValidPassword(password) && !string.IsNullOrWhiteSpace(fullName))
        {
            AccountModel account = new AccountModel(email, password, fullName);
            AccountsAccess.Write(account);

            return account;
        }

        Console.WriteLine("Could not register account because:");

        if (!IsValidEmail(email))
            Console.WriteLine("- The email address is invalid.");

        if (!IsValidPassword(password))
            Console.WriteLine("- The password must be at least 8 characters long.");

        if (string.IsNullOrWhiteSpace(fullName))
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




