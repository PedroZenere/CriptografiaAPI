using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace CriptografiaAPI.Criptografar
{
    public class CriptografiaService : ICriptografiaService
    {
        private readonly IConfiguration _configuration;
        private readonly byte[] vetorIV = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54, 0x32, 0x10 };
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        private static string key;
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
        public CriptografiaService(IConfiguration configuration) 
        { 
            _configuration = configuration;
            key = _configuration["Crypt:Key"];
        }
        public string EncryptString(string plainText)
        {   
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                byte[] v = Encoding.UTF8.GetBytes(key);
                aesAlg.Key = v;
                aesAlg.IV = vetorIV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using(StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
     
            return Convert.ToBase64String(encrypted);
        }

        public string DecryptString(string cipherText)
        {
            string plaintext;

            using (Aes aesAlg = Aes.Create())
            {
                byte[] v = Encoding.UTF8.GetBytes(key);
                aesAlg.Key = v;
                aesAlg.IV = vetorIV;

                var cipherByte = Convert.FromBase64String(cipherText);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherByte))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            };

            return plaintext;
        }
    }
}