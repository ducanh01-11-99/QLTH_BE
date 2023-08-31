using System.Text.RegularExpressions;

namespace QLTH.Utilities
{
    public static class ValidatePassword
    {
        public static bool Validate(string password)
        {
            // Tạo một regex để kiểm tra mật khẩu
            string regex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*()_+<>?]).{8,}$";

            // Tạo một chuỗi mật khẩu

            // Kiểm tra mật khẩu
            if (Regex.IsMatch(password, regex))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
