using System;

namespace InfoPuls.Model.Entity
{
    public class User
    {
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public User() { }

        public User(string email, string lastName, string firtstName, DateTime dateOfBirth) : this()
        {
            this.Email = email;
            this.LastName = lastName;
            this.FirstName = firtstName;
            this.DateOfBirth = dateOfBirth;
        }

        public override string ToString()
        {
            string format = "Email: {0}, LastName: {1}, FirstName: {2}, DateOfBirth: {3}";
            return String.Format(format, this.Email, this.LastName, this.FirstName, this.DateOfBirth.ToString("d"));
        }

        public override int GetHashCode()
        {
            return this.ToString().ToLower().GetHashCode();
        }
    }
}
