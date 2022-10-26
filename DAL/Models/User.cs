namespace DAL.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public Role Role { get; set; }
    }
}