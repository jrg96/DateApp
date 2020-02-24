using System;
using System.Collections.Generic;

namespace DateApp.API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string DateOfBirth { get; set; }

        public string KnownAs { get; set; }

        public DateTime MyProperty { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}