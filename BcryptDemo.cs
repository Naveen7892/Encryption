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
    // https://blog.filippo.io/salt-and-pepper/
    // https://www.javacodegeeks.com/2012/08/bcrypt-salt-its-bare-minimum.html

    #endregion References

    class BcryptDemo {
        static void Main (string[] args) {
            string hashed = HashPassword ("my_password");
            // sample hash: $2a$12$RsecSrzLJrUPUaKD6c.64.4oZ/WQzpOq7X/EXcPkZ46oJgW/34rcq (generates diff pass for same input but validates correctly for all hash. Maths is great!!!)

            Console.WriteLine (hashed);

            if (ValidatePassword ("my_password", hashed)) {

                if (ValidatePassword ("my_password", "$2a$12$d/JKWxBtChGWAQlzf4eUGeGUFRUY15/nUED0vsIcVU7Wfk.bGi92m")) {
                    Console.WriteLine ("Correct - 2");
                }

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
       
