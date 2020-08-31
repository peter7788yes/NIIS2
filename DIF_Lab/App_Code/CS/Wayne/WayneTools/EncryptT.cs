using System;
using System.Security.Cryptography;
using System.Text;

namespace WayneTools
{
    /// <summary>
    /// EncryptT 的摘要描述
    /// </summary>
    public class EncryptT
    {
        public EncryptT()
        {
            //
            // TODO: 在這裡新增建構函式邏輯
            //
        }

        /// <summary>
        /// MD5不建議使用
        /// </summary>
        /// <param name="text"></param>
        public string ToMD5(string text)
        {
            MD5 md5 = MD5.Create();//建立一個MD5
            byte[] source = Encoding.Default.GetBytes(text);//將字串轉為Byte[]
            byte[] crypto = md5.ComputeHash(source);//進行MD5加密
            return Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
        }

        /// <summary>
        /// SHA1不建議使用
        /// </summary>
        /// <param name="text"></param>
        public string ToSHA1(string text)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();//建立一個SHA1
            byte[] source = Encoding.Default.GetBytes(text);//將字串轉為Byte[]
            byte[] crypto = sha1.ComputeHash(source);//進行SHA1加密
            return Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
        }


        /// <summary>
        /// SHA256,256 bits 加密
        /// </summary>
        /// <param name="text"></param>
        public string ToSHA256(string text)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();//建立一個SHA256
            byte[] source = Encoding.Default.GetBytes(text);//將字串轉為Byte[]
            byte[] crypto = sha256.ComputeHash(source);//進行SHA256加密
            return  Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
        }


        /// <summary>
        /// SHA384,384 bits 加密
        /// </summary>
        /// <param name="text"></param>
        public string ToSHA384(string text)
        {
            SHA384 sha384 = new SHA384CryptoServiceProvider();//建立一個SHA384
            byte[] source = Encoding.Default.GetBytes(text);//將字串轉為Byte[]
            byte[] crypto = sha384.ComputeHash(source);//進行SHA384加密
            return  Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
        }


        /// <summary>
        /// SHA512,512 bits 加密
        /// </summary>
        /// <param name="text"></param>
        public string ToSHA512(string text)
        {
            SHA512 sha512 = new SHA512CryptoServiceProvider();//建立一個SHA512
            byte[] source = Encoding.Default.GetBytes(text);//將字串轉為Byte[]
            byte[] crypto = sha512.ComputeHash(source);//進行SHA512加密
            return Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
        }
    }
}