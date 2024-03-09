using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TempporalWS.Models;

namespace TempporalWS
{
    public class Encrypt
    {
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static byte[] pwdHash;
        public static byte[] pwdSalt;
        public static void CrearPasswordHash512(string password)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512();
            pwdSalt = hmac.Key;
            pwdHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        }

        public static bool VerificarPassword(Usuario user, string password)
        {
            return (Encrypt.GetSHA256(password).Equals(user.Password));
        }

        public static string AppSettingsReader(string Firstkey, string SecondKey)
        {
            try
            {
                var stringReader = new StreamReader(File.OpenRead("/app/appsettings.json"));
                string jsonConfig = "";
                jsonConfig = stringReader.ReadToEnd();
                var reader = JObject.Load(new JsonTextReader(new StringReader(jsonConfig)));
                return reader.GetValue(Firstkey).Value<string>(SecondKey);
            }
            catch { return ""; }
        }

    }
}
