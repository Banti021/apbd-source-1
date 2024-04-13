using System;

namespace LegacyApp.Interfaces;

public interface ICreditLimitStrategy
{
    bool AppliesTo(string clientType);
    void SetCreditLimit(User user, DateTime dateOfBirth);
}