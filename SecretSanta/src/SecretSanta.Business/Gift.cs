using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Business
{
    public class Gift
    {
        public int Id { get; }
        private string _Title;
        private string _Description;
        private string _Url;
        public User User { get; set; }
        public Gift(int id, string title, string description, string url, User user)
        {
            Id = id;
            Title = title;
            Description = description;
            Url = url;
            User = user;
        }

        public string Title
        {
            get => _Title;
            set => _Title = value ?? throw new ArgumentNullException(nameof(value));
        }
        public string Description
        {
            get => _Description;
            set => _Description = value ?? throw new ArgumentNullException(nameof(value));
        }
        public string Url
        {
            get => _Url;
            set => _Url = value ?? throw new ArgumentNullException(nameof(value));
        }

    }
}
