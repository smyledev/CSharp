using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptLab1WebAppMVC
{
    public class DigitalSignature
    {
        static ECDsaCng ecsdKeyVerify;
        static ECDsaCng ecsdKey;

        public static bool VerifyData(string receiverDirPath, string keysDir)
        {
            var publickey = System.IO.File.ReadAllBytes(receiverDirPath + keysDir + "PublicKey.db");
            byte[] message = System.IO.File.ReadAllBytes(receiverDirPath + keysDir + "Data.txt");
            byte[] signature = System.IO.File.ReadAllBytes(receiverDirPath + keysDir + "Signature.db");

            if (ecsdKeyVerify == null)
            {
                ecsdKeyVerify = new ECDsaCng(CngKey.Import(publickey, CngKeyBlobFormat.EccPublicBlob));
                ecsdKeyVerify.HashAlgorithm = CngAlgorithm.Sha512;
            }

            if (ecsdKeyVerify.VerifyData(message, signature))
                return true;
            else
                return false;
        }

        public static byte[] GetSignature(byte[] privatekey, byte[] msg)
        {
            if (ecsdKey == null)
            {
                ecsdKey = new ECDsaCng(CngKey.Import(privatekey, CngKeyBlobFormat.EccPrivateBlob));
                ecsdKey.HashAlgorithm = CngAlgorithm.Sha512;
            }
            
            byte[] signature = ecsdKey.SignData(msg);

            if (ecsdKey.VerifyData(msg, signature))
                return signature;
            else
                throw new Exception("Data Verify Failed!");
        }

        public static void CreateECDKey(out byte[] PrivateKey, out byte[] PublicKey, string KeyName = "Ключшифрования", string keyAlias = "AdminKey")
        {
            var p = new CngKeyCreationParameters
            {
                ExportPolicy = CngExportPolicies.AllowPlaintextExport,
                KeyCreationOptions = CngKeyCreationOptions.OverwriteExistingKey,
                UIPolicy = new CngUIPolicy(CngUIProtectionLevels.ProtectKey, KeyName, null, null, null)
            };
            var key = CngKey.Create(CngAlgorithm.ECDsaP521, keyAlias, p);
            using (ECDsaCng dsa = new ECDsaCng(key))
            {
                dsa.HashAlgorithm = CngAlgorithm.Sha512;
                PublicKey = dsa.Key.Export(CngKeyBlobFormat.EccPublicBlob);
                PrivateKey = dsa.Key.Export(CngKeyBlobFormat.EccPrivateBlob);
            }
        }

        public static void CreateKeys(string senderDirPath, string keysDir)
        {
            if (!System.IO.File.Exists(senderDirPath + keysDir + "PrivateKey.db"))
            {
                byte[] private_key, public_key;
                CreateECDKey(out private_key, out public_key);
                System.IO.File.WriteAllBytes(senderDirPath + keysDir + "PrivateKey.db", private_key);
                System.IO.File.WriteAllBytes(senderDirPath + keysDir + "PublicKey.db", public_key);
            }
        }

        public static void CreateSignature(string senderDirPath, string keysDir, string text)
        {
            // Если подписи нет, создаем ее
            if (!System.IO.File.Exists(senderDirPath + keysDir + "Signature.db"))
            {
                // Если закрытого ключа нет, создаем его
                if (!System.IO.File.Exists(senderDirPath + keysDir + "PrivateKey.db"))
                {
                    byte[] private_key, public_key;
                    CreateECDKey(out private_key, out public_key);
                    System.IO.File.WriteAllBytes(senderDirPath + keysDir + "PrivateKey.db", private_key);
                    System.IO.File.WriteAllBytes(senderDirPath + keysDir + "PublicKey.db", public_key);
                }

                byte[] msg = Encoding.Default.GetBytes(text);

                var privatekey = System.IO.File.ReadAllBytes(senderDirPath + keysDir + "PrivateKey.db");
                var signature = GetSignature(privatekey, msg);
                System.IO.File.WriteAllBytes(senderDirPath + keysDir + "Signature.db", signature);
                System.IO.File.WriteAllBytes(senderDirPath + keysDir + "Data.txt", msg);
            }
        }

    }
}