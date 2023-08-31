using System.Text.RegularExpressions;

namespace QLTH.Utilities
{
    public class ValidatePhone
    {
        public static bool Validate(string phoneNumber)
        {
            // Tạo một regex để kiểm tra mật khẩu
            string regex = @"^[+84]?[0-9]{9}$";

            // Tạo một chuỗi mật khẩu

            // Kiểm tra mật khẩu
            if (Regex.IsMatch(phoneNumber, regex))
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
