using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace ConsoleApplication2
{
    class DIFFIE_HELMAN
    {
        #region FIELDS
        private AesCryptoServiceProvider AES = new AesCryptoServiceProvider();
        private ECDiffieHellmanCng keyManager = new ECDiffieHellmanCng();
        private byte[] publicKey = null;
        public byte[] PublicKey
        {
            get
            {
                return publicKey;
            }
            private set
            {
                publicKey = value;
            }
        }

        public byte[] secretKey = null;
        private byte[] IV = null;
        public byte[] algo_IV
        {
            get
            {
                return IV;
            }
            private set
            {
                IV = value;
            }
        }
        #endregion

        #region CONSTRUCTORS

        public DIFFIE_HELMAN() {
            keyManager.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            keyManager.HashAlgorithm = CngAlgorithm.Sha256;
            publicKey = keyManager.PublicKey.ToByteArray();
        }
        #endregion


        #region METHODS
        public void CreateSecretKey(byte[] publicKey)
        {
            CngKey key = CngKey.Import(publicKey, CngKeyBlobFormat.EccPublicBlob);
            secretKey = keyManager.DeriveKeyMaterial(key);
        }

        public byte[] Send(byte[] key, byte[] message)
        {
            AES.Key = secretKey;
            IV = AES.IV;

            using (MemoryStream ciphertext = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ciphertext, AES.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(message, 0, message.Length);
                cs.Close();
                return ciphertext.ToArray();
            }


        }

        public byte[] Receive(byte[] message, byte[] iv)
        {
            AES.Key = secretKey;
            AES.IV = iv;
            // Decrypt the message
            using (MemoryStream plaintext = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(plaintext, AES.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(message, 0, message.Length);
                    cs.Close();
                    return plaintext.ToArray();
                }
            }
        }
        #endregion
    }
}
