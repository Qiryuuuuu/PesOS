using System;

namespace PesOS
{
    [Serializable]
    public class User
    {
        public string Username { get; private set; }
        private string PasswordHash { get; set; }
        public string HostIdentityCode { get; private set; }

        public User(string username, string password)
        {
            Username = username;
            PasswordHash = HashPassword(password);
            HostIdentityCode = Guid.NewGuid().ToString();
        }

        private string HashPassword(string password)
        {
            // Use a secure hashing algorithm (e.g., bcrypt) in a real-world scenario
            return password.GetHashCode().ToString();
        }

        public bool ValidatePassword(string inputPassword)
        {
            // Use a secure password verification mechanism in a real-world scenario
            return PasswordHash == HashPassword(inputPassword);
        }

        public void ChangePassword(string newPassword)
        {
            PasswordHash = HashPassword(newPassword);
        }

        public void ChangeUsername(string newUsername)
        {
            Username = newUsername;
        }

        public void ChangeHostIdentity()
        {
            HostIdentityCode = Guid.NewGuid().ToString();
        }

        public bool Logout()
        {
            // Perform any logout-specific actions here
            Console.WriteLine($"User {Username} has been logged out.");

            // Returning true indicates that the user successfully logged out
            return true;
        }

        public static User CreateUser(string username, string password)
        {
            // Create a new user without saving it
            return new User(username, password);
        }

        public void DisplayUserInfo()
        {
            Console.WriteLine($"Username: {Username}");
            Console.WriteLine($"Host Identity Code: {HostIdentityCode}");
        }
    }
    
}
