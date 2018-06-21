using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Eagles.Base.DesEncrypt.Implement
{
    public class DesEncrypt:IDesEncrypt
    {
        private const string DesKey = "asdjnahz";

        private const CipherMode Cipher = CipherMode.CBC;

        private const PaddingMode Padding = PaddingMode.PKCS7;

        public string Encrypt(string str)
        {
            var keyBytes = Encoding.UTF8.GetBytes(DesKey.Substring(0, 8));
            var keyIv = keyBytes;
            var inputByteArray = Encoding.UTF8.GetBytes(str);
            var provider = new DESCryptoServiceProvider();
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIv), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        public string Decrypt(string str)
        {
            var keyBytes = Encoding.UTF8.GetBytes(DesKey.Substring(0, 8));
            var keyIv = keyBytes;
            var inputByteArray = Convert.FromBase64String(str);
            var provider = new DESCryptoServiceProvider();
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIv), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }

        public string EncryptToHex(string str)
        {
            //为了安全级别更高，这个地方建议将密钥进行MD5后取8位，这里我就不加此方法啦  
            //var keyBytes = encoding.GetBytes(Md5Helper.GetMd5HashString(encryptKey, encoding).Substring(0, 8));  
            var keyBytes = Encoding.UTF8.GetBytes(DesKey);
            var inputBytes = Encoding.UTF8.GetBytes(str);
            var outputBytes = EncryptToDesBytes(inputBytes, keyBytes, Cipher, Padding);
            var sBuilder = new StringBuilder();
            foreach (var b in outputBytes)
            {
                sBuilder.Append(b.ToString("X2"));
            }
            return sBuilder.ToString();
        }

        public string DecryptToHex(string str)
        {
            //为了安全级别更高，这个地方建议将密钥进行MD5后取8位，这里我就不加此方法啦  
            //var keyBytes = encoding.GetBytes(Md5Helper.GetMd5HashString(encryptKey, encoding).Substring(0, 8));  
            var keyBytes = Encoding.UTF8.GetBytes(DesKey);
            var inputBytes = new byte[str.Length / 2];
            for (var i = 0; i < inputBytes.Length; i++)
            {
                inputBytes[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
            }
            var outputBytes = DecryptByDesBytes(inputBytes, keyBytes, Cipher, Padding);
            return Encoding.UTF8.GetString(outputBytes).TrimEnd('\0');
        }

        #region DES 加密  

        /// <summary>  
        /// 加密
        /// </summary>  
        /// <param name="encryptBytes">待加密的字节数组</param>  
        /// <param name="keyBytes">加密密钥字节数组</param>  
        /// <param name="cipher">运算模式</param>  
        /// <param name="padding">填充模式</param>  
        /// <returns></returns>  
        private IEnumerable<byte> EncryptToDesBytes(byte[] encryptBytes, byte[] keyBytes,
            CipherMode cipher = CipherMode.ECB, PaddingMode padding = PaddingMode.Zeros)
        {
            var des = new DESCryptoServiceProvider
            {
                Key = keyBytes,
                IV = keyBytes,
                Mode = cipher,
                Padding = padding
            };
            var outputBytes = des.CreateEncryptor().TransformFinalBlock(encryptBytes, 0, encryptBytes.Length);
            return outputBytes;
        }

        #endregion

        #region DES 解密 


        /// <summary>  
        /// 解密
        /// </summary>  
        /// <param name="decryptBytes">待解密的字节数组</param>  
        /// <param name="keyBytes">解密密钥字节数组</param>  
        /// <param name="cipher">运算模式</param>  
        /// <param name="padding">填充模式</param>  
        /// <returns></returns>  
        private byte[] DecryptByDesBytes(byte[] decryptBytes, byte[] keyBytes,
            CipherMode cipher = CipherMode.ECB, PaddingMode padding = PaddingMode.Zeros)
        {
            var des = new DESCryptoServiceProvider
            {
                Key = keyBytes,
                IV = keyBytes,
                Mode = cipher,
                Padding = padding
            };
            var outputBytes = des.CreateDecryptor().TransformFinalBlock(decryptBytes, 0, decryptBytes.Length);
            return outputBytes;
        }

        #endregion
    }
}
