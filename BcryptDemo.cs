using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryption {

   // namespace alias. Not working
   // using Bcr = BCrypt.Net.BCrypt;

   using BCrypt.Net;

   #region References

   // http://bcrypt.codeplex.com/documentation
   // https://cmatskas.com/a-simple-net-password-hashing-implementation-using-bcrypt/
   // https://github.com/BcryptNet/bcrypt.net/issues/11

   #endregion References

   class BcryptDemo {
      static void Main(string[] args) {
         string hashed = HashPassword ("my_password");
         Console.WriteLine (hashed);

         if(ValidatePassword("my_password", hashed)) {
            Console.WriteLine ("Correct");
         } else {
            Console.WriteLine ("False");
         }

         Console.ReadLine ();
      }

      private static string GetRandomSalt () {
         // Error: the type or namespace name 'GenerateSalt' does not exist in the namespace
         // return BCrypt.GenerateSalt (12);

         return BCrypt.GenerateSalt (12);
      }

      public static string HashPassword (string password) {
         return BCrypt.HashPassword (password, GetRandomSalt ());
      }

      public static bool ValidatePassword (string password, string correctHash) {
         return BCrypt.Verify (password, correctHash);
      }
   }
}
