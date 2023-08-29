namespace QLTH.Models.DTO
{
    public class UserDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Email { get; set; }
        public int isActive { get; set; }

        public UserDTO(string userName, string passWord, string email, int isactive) {
            Username = userName;
            Password = passWord;
            Email = email;
            isActive = isactive;
        }
    }
}
