using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

public class QueryStringEncryptToolS
{
    public QueryStringEncryptToolS()
	{
	}

    public static string Encrypt(string text)
    {
        string key = WebConfigurationManager.AppSettings["QueryStringEncryptKey"].ToString();
        string salt = WebConfigurationManager.AppSettings["QueryStringEncryptSalt"].ToString();

        return QueryStringEncryptToolS.EncryptString(text, key, salt);
    }

    public static string Decrypt(string text)
    {
        string key = WebConfigurationManager.AppSettings["QueryStringEncryptKey"].ToString();
        string salt = WebConfigurationManager.AppSettings["QueryStringEncryptSalt"].ToString();

        return QueryStringEncryptToolS.DecryptString(text, key, salt);
    }

    public static string EncryptString(string inputText, string key, string salt)
    {
        try
        {
            byte[] plainText = Encoding.UTF8.GetBytes(inputText);

            using (RijndaelManaged rijndaelCipher = new RijndaelManaged())
            {
                PasswordDeriveBytes secretKey = new PasswordDeriveBytes(Encoding.ASCII.GetBytes(key), Encoding.ASCII.GetBytes(salt));
                using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(plainText, 0, plainText.Length);
                            cryptoStream.FlushFinalBlock();
                            string base64 = Convert.ToBase64String(memoryStream.ToArray());

                            return base64;
                        }
                    }
                }
            }
        }
        catch
        {
            return "";
        }
    }


    public static string EncryptToQueryString(string inputText, string key, string salt)
    {
        return HttpUtility.UrlEncode(EncryptString(inputText,key,salt));
    }

    public static string DecryptString(string inputText, string key, string salt)
    {
        try
        {
            byte[] encryptedData = Convert.FromBase64String(inputText);
            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(Encoding.ASCII.GetBytes(key), Encoding.ASCII.GetBytes(salt));

            using (RijndaelManaged rijndaelCipher = new RijndaelManaged())
            {
                using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
                {
                    using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {

                            byte[] plainText = new byte[encryptedData.Length];
                            cryptoStream.Read(plainText, 0, plainText.Length);
                            string utf8 = Encoding.UTF8.GetString(plainText);
                            return utf8;

                        }
                    }
                }
            }
        }
        catch
        {
            return "";
        }
    }
}