using System;
using System.Collections.Generic;
using System.Text;
using VPassSample.Models;

namespace VPassManager
{
    public static class utils
    {
        public static CUsuario User;
        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
