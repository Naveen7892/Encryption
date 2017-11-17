using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encryption {

   #region References

   #endregion References

   class AES_Rijndael {
      static void Main (string[] args) {
         try {

            //string original = "Here is some data to encrypt!";

            string original = "3223-6213-1360-7021-6113";

            // Create a new instance of the RijndaelManaged
            // class.  This generates a new key and initialization 
            // vector (IV).
            using (RijndaelManaged myRijndael = new RijndaelManaged ()) {

               myRijndael.GenerateKey ();
               myRijndael.GenerateIV ();

               #region Not Working 1

               //myRijndael.Key = System.Text.Encoding.UTF8.GetBytes ("Testing");
               //myRijndael.IV = System.Text.Encoding.UTF8.GetBytes ("TestingIV");

               #endregion Not Working 1

               #region ConvertUTFtoString Helper Fn

               //System.Text.Encoding utf_8 = System.Text.Encoding.UTF8;

               //// This is our Unicode string:
               //string s_unicode = "abcéabc";

               //// Convert a string to utf-8 bytes.
               //byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes (s_unicode);

               //// Convert utf-8 bytes to a string.
               //string s_unicode2 = System.Text.Encoding.UTF8.GetString (utf8Bytes);

               //MessageBox.Show (s_unicode2);

               #endregion ConvertUTFtoString Helper Fn

               // Encrypt the string to an array of bytes.
               byte[] encrypted = EncryptStringToBytes (original, myRijndael.Key, myRijndael.IV);

               string s_unicode2 = System.Text.Encoding.UTF8.GetString (encrypted);

               // Decrypt the bytes to a string.
               string roundtrip = DecryptStringFromBytes (encrypted, myRijndael.Key, myRijndael.IV);

               //Display the original data and the decrypted data.
               Console.WriteLine ("Original:   {0}", original);
               Console.WriteLine ("Encrypted: {0}", s_unicode2);
               Console.WriteLine ("Round Trip: {0}", roundtrip);

               Console.ReadKey ();
            }

         } catch (Exception e) {
            Console.WriteLine ("Error: {0}", e.Message);
         }
      }

      static byte[] EncryptStringToBytes (string plainText, byte[] Key, byte[] IV) {
         // Check arguments.
         if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException ("plainText");
         if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException ("Key");
         if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException ("IV");
         byte[] encrypted;
         // Create an RijndaelManaged object
         // with the specified key and IV.
         using (RijndaelManaged rijAlg = new RijndaelManaged ()) {
            rijAlg.Key = Key;
            rijAlg.IV = IV;

            // Create a decrytor to perform the stream transform.
            ICryptoTransform encryptor = rijAlg.CreateEncryptor (rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream ()) {
               using (CryptoStream csEncrypt = new CryptoStream (msEncrypt, encryptor, CryptoStreamMode.Write)) {
                  using (StreamWriter swEncrypt = new StreamWriter (csEncrypt)) {

                     //Write all data to the stream.
                     swEncrypt.Write (plainText);
                  }
                  encrypted = msEncrypt.ToArray ();
               }
            }
         }


         // Return the encrypted bytes from the memory stream.
         return encrypted;

      }

      static string DecryptStringFromBytes (byte[] cipherText, byte[] Key, byte[] IV) {
         // Check arguments.
         if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException ("cipherText");
         if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException ("Key");
         if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException ("IV");

         // Declare the string used to hold
         // the decrypted text.
         string plaintext = null;

         // Create an RijndaelManaged object
         // with the specified key and IV.
         using (RijndaelManaged rijAlg = new RijndaelManaged ()) {
            rijAlg.Key = Key;
            rijAlg.IV = IV;

            // Create a decrytor to perform the stream transform.
            ICryptoTransform decryptor = rijAlg.CreateDecryptor (rijAlg.Key, rijAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream (cipherText)) {
               using (CryptoStream csDecrypt = new CryptoStream (msDecrypt, decryptor, CryptoStreamMode.Read)) {
                  using (StreamReader srDecrypt = new StreamReader (csDecrypt)) {

                     // Read the decrypted bytes from the decrypting stream
                     // and place them in a string.
                     plaintext = srDecrypt.ReadToEnd ();
                  }
               }
            }

         }

         return plaintext;

      }
   }
}
