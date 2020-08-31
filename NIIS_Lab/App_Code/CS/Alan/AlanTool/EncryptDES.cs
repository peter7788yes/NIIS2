using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace AlanTools
{
    /// <summary>
    /// EncryptDES 的摘要描述
    /// </summary>
    public class EncryptDES
    {
        public EncryptDES()
        {
            //
            // TODO: 在這裡新增建構函式邏輯
            //
        }
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        ///待加密的字符串 ///加密密钥,要求为8位 /// 加密成功返回加密后的字符串，失败返回null
        public string ToEncryptDES(string encryptString, string encryptKey)//Smaple encryptKey = "11001100" 密碼金鑰
        {
            try
            {
                byte[] rgbKey = ASCIIEncoding.ASCII.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = rgbKey;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                foreach (byte b in mStream.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                ret.ToString();
                return ret.ToString();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="decryptKey"></param>
        ///待解密的字符串 ///解密密钥,要求为8位,和加密密钥相同 /// 解密成功返回解密后的字符串，失败返回null
        public string ToDecryptDES(string decryptString, string decryptKey)//Smaple decryptKey = "11001100" 密碼金鑰
        {
            try
            {
                byte[] rgbKey = ASCIIEncoding.ASCII.GetBytes(decryptKey);
                byte[] rgbIV = rgbKey;
                byte[] inputByteArray = new byte[decryptString.Length / 2];
                for (int x = 0; x < decryptString.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(decryptString.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return null;
            }
        }
    }
}