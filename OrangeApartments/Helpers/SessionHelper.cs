using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using OrangeApartments.Core.Domain;

namespace OrangeApartments.Helpers
{
    public static class SessionHelper
    {
        private static Dictionary<string, int> _sessions = new Dictionary<string, int>();
        

        public static string CreateSession(int userId)
        {
            try
            {
                var token = CreateToken(userId);
                _sessions.Add(token, userId);
                return token;
            }
            catch (Exception e)
            {
                return "Error during create session";
            }
            
        }

        public static string CreateSession(User user)
        {
            return CreateSession(user.UserId);
        }

        private static string CreateToken(int userId)
        {
            return CalculateMD5Hash($"{userId}:{DateTime.UtcNow}");
        }

        public static int GetSession(string token)
        {
            return _sessions.ContainsKey(token) ? _sessions[token] : -1;
        }

        private static string CalculateMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static void ClearAllSessions()
        {
            _sessions.Clear();
        }

        public static bool ClearSession(string token)
        {
            if (!_sessions.ContainsKey(token)) return false;

            _sessions.Remove(token);
            return true;
        }
    }
}