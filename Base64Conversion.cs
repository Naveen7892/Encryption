using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption {

   #region References

   //https://arcanecode.com/2007/03/21/encoding-strings-to-base64-in-c/

   #endregion References

   class Base64Conversion {
      static void MainBase64Conversion (string[] args) {
         string myData = "3223-6213-1360-7021-6113";

         string myDataEncoded = EncodeTo64 (myData);
         Console.WriteLine (myDataEncoded);

         string myDataUnencoded = DecodeFrom64 (myDataEncoded);
         Console.WriteLine (myDataUnencoded);

         Console.ReadLine ();
      }

      static public string EncodeTo64 (string toEncode) {
         byte[] toEncodeAsBytes
               = System.Text.ASCIIEncoding.ASCII.GetBytes (toEncode);
         string returnValue
               = System.Convert.ToBase64String (toEncodeAsBytes);
         return returnValue;
      }


      static public string DecodeFrom64 (string encodedData) {
         byte[] encodedDataAsBytes
             = System.Convert.FromBase64String (encodedData);
         string returnValue =
            System.Text.ASCIIEncoding.ASCII.GetString (encodedDataAsBytes);
         return returnValue;
      }

   }
}
