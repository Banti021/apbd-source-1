using System;
using LegacyApp.Interfaces;

namespace LegacyApp.CreditLimitStrategies;

public class VeryImportantClientStrategy : ICreditLimitStrategy
{
    public bool AppliesTo(string clientType)
    {
        return clientType == "VeryImportantClient";
    }

    public void SetCreditLimit(User user, DateTime dateOfBirth)
    {
        user.HasCreditLimit = false;
    }
}