using System;
using LegacyApp.Interfaces;

namespace LegacyApp.CreditLimitStrategies;

public class ImportantClientStrategy : ICreditLimitStrategy
{
    public bool AppliesTo(string clientType)
    {
        return clientType == "ImportantClient";
    }

    public void SetCreditLimit(User user, DateTime dateOfBirth)
    {
        using (var userCreditService = new UserCreditService())
        {
            int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            user.CreditLimit = creditLimit * 2;
        }
        user.HasCreditLimit = true;
    }
}