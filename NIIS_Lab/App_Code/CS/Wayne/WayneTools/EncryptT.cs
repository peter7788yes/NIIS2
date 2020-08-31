using System;
using System.Security.Cryptography;
using System.Text;

namespace WayneTools
{
    public class EncryptT
    {
        public EncryptT()
        {
        }

        public string ToMD5(string text)
        {
            MD5 md5 = MD5.Create();
            byte[] source = Encoding.Default.GetBytes(text);
            byte[] crypto = md5.ComputeHash(source);
            return Convert.ToBase64String(crypto);
        }

        public string ToSHA1(string text)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] source = Encoding.Default.GetBytes(text);
            byte[] crypto = sha1.ComputeHash(source);
            return Convert.ToBase64String(crypto);
        }

        public string ToSHA256(string text)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] source = Encoding.Default.GetBytes(text);
            byte[] crypto = sha256.ComputeHash(source);
            return  Convert.ToBase64String(crypto);
        }

        public string ToSHA384(string text)
        {
            SHA384 sha384 = new SHA384CryptoServiceProvider();
            byte[] source = Encoding.Default.GetBytes(text);
            byte[] crypto = sha384.ComputeHash(source);
            return  Convert.ToBase64String(crypto);
        }

        public string ToSHA512(string text)
        {
            SHA512 sha512 = new SHA512CryptoServiceProvider();
            byte[] source = Encoding.Default.GetBytes(text);
            byte[] crypto = sha512.ComputeHash(source);
            return Convert.ToBase64String(crypto);
        }
    }
}