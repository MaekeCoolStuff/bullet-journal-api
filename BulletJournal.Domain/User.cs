using System;
using System.Collections.Generic;

namespace BulletJournal.Domain
{
    public class User
    {
        public User()
        {
            Items = new List<Item>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Item> Items { get; set; }
    }
}
