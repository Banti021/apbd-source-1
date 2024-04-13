using System;
using System.Collections.Generic;
using System.Linq;
using LegacyApp.Interfaces;
using LegacyApp.Validators;
using LegacyApp.CreditLimitStrategies;

namespace LegacyApp
{
    public class UserService
    {
        protected readonly UserValidator UserValidator = new();
        protected readonly IClientRepository ClientRepository;
        private List<ICreditLimitStrategy> _creditLimitStrategies;
        
        
        public UserService(IClientRepository clientRepository)
        {
            ClientRepository = clientRepository;
            InitializeStrategies();
        }

        public UserService() : this(new ClientRepository())
        {
        }

        private void InitializeStrategies()
        {
            _creditLimitStrategies = new List<ICreditLimitStrategy>
            {
                new VeryImportantClientStrategy(), 
                new ImportantClientStrategy(), 
                new DefaultClientStrategy()
            };
        }


        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!UserValidator.ValidateUser(firstName, lastName, email, dateOfBirth))
            {
                return false;
            }

            var client = ClientRepository.GetById(clientId);
            var user = CreateUser(firstName, lastName, email, dateOfBirth, client);
        
            ApplyCreditLimitStrategy(user, client);

            if (!CanUserBeAdded(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
        
        private User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client)
        {
            return new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
        }

        private void ApplyCreditLimitStrategy(User user, Client client)
        {
            var strategy = _creditLimitStrategies.FirstOrDefault(s => s.AppliesTo(client.Type));
            strategy?.SetCreditLimit(user, user.DateOfBirth);
        }

        private bool CanUserBeAdded(User user)
        {
            return !(user.HasCreditLimit && user.CreditLimit < 500);
        }
    }
}
