using System;

namespace LegacyApp.Validators;

public class UserValidator
{
    public bool ValidateUser(string firstName, string lastName, string email, DateTime dateOfBirth)
    {
        return IsNameValid(firstName, lastName) &&
               IsEmailValid(email) &&
               IsUserAdult(dateOfBirth);
    }
    
    private bool IsUserAdult(DateTime dateOfBirth)
    {
        var now = DateTime.Now;
        int age = now.Year - dateOfBirth.Year;
        if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day))
        {
            age--;
        }
        return age >= 21;
    }

    private bool IsEmailValid(string email)
    {
        return email.Contains('@') && email.Contains('.');
    }

    private bool IsNameValid(string firstName, string lastName)
    {
        return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName);
    }
}