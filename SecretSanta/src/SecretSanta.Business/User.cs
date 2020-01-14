using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    public class User
    {
        public int Id { get; }
        private string _FirstName;
        private string _LastName;
        public List<Gift> Gift { get; }
        public User(int id, string firstName, string lastName, List<Gift> gift)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gift = gift;
        }

        public string FirstName 
        {
            get => _FirstName;
            set => _FirstName = value ?? throw new ArgumentNullException(nameof(value));
        }
        public string LastName
        {
            get => _LastName;
            set => _LastName= value ?? throw new ArgumentNullException(nameof(value));
        }

    }
}
