using System;
using LegacyApp.Interfaces;

namespace LegacyApp.CreditLimitStrategies;

public class DefaultClientStrategy : ICreditLimitStrategy
{
    public bool AppliesTo(string clientType)
    {
        return true;
    }

    public void SetCreditLimit(User user, DateTime dateOfBirth)
    {
        user.HasCreditLimit = true;
        using (var userCreditService = new UserCreditService())
        {
            int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            user.CreditLimit = creditLimit;
        }
    }
}