using System;
using InfoPuls.Model.Entity;
using InfoPuls.Model.Tools;

namespace InfoPuls.Model.Test
{
    public class UserBuilder
    {
        private string Email = "example@mail.com";
        private string LastName = "last";
        private string FirstName = "first";
        private DateTime DateOfBirth = Helper.GetDateInUsFormat("4/30/1955");

        public User Build()
        {
            return new User(Email, LastName, FirstName, DateOfBirth);
        }

        public UserBuilder WithFirstName(string firstname)
        {
            this.FirstName = firstname;
            return this;
        }

        public UserBuilder WithLastName(string lastname)
        {
            this.LastName = lastname;
            return this;
        }

        public UserBuilder WithBirthDate(string birthdate)
        {
            this.DateOfBirth = Helper.GetDateInUsFormat(birthdate);
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            this.Email = email;
            return this;
        }

        public static implicit operator User(UserBuilder instance)
        {
            return instance.Build();
        }
    }
}
