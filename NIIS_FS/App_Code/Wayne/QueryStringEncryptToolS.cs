using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Class1 的摘要描述
/// </summary>
public class QueryStringEncryptToolS
{
    public QueryStringEncryptToolS()
	{
		//
		// TODO: 在這裡新增建構函式邏輯
		//
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

                            //URL encode 一次，才不會被截斷
                            // Generate a string that won't get screwed up when passed as a query string.
                            //string urlEncoded = HttpUtility.UrlEncode(base64);
                            //return urlEncoded;

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