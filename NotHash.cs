using System;
using System.Text;
using System.Security.Cryptography;

namespace NotHash
{
    public class NotHash
    {
        //MAX STRING LENGHT IS 509 CHARACTERS DONT USE MORE !
        public string NotHasher(string rawInput)
        {
            string returnString = "";
            try
            {
                int lenght = rawInput.Length;
                char first, last, special;

                if (lenght < 8) throw new Exception("Lenght cannot be less then 8");

                first = Convert.ToChar(rawInput.Substring(0, 1));
                last = Convert.ToChar(rawInput.Substring(lenght - 1, 1));
                //SPECIAL
                #region special
                int specialInt = 0;
                if(lenght < 24)
                {
                    specialInt = 12;
                }
                if(lenght < 96 && lenght >= 24)
                {
                    specialInt = 90;
                }
                if(lenght < 200 && lenght >= 96)
                {
                    specialInt = 198;
                }
                if(lenght >= 200)
                {
                    specialInt = 199;
                }
                special = Convert.ToChar(rawInput.Substring(specialInt, 1));

                #endregion
                //SPECIAL END

                //HASHING
                SHA256 sha256 = new SHA256CryptoServiceProvider();

                string hashedString = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(rawInput)));
                
                char[] firstLastChar = { first, last };
                string firstLastChars = new string(firstLastChar);

                string hashedFirstLast = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(firstLastChars)));
                
                string specialHashPrep = hashedFirstLast + Convert.ToString(special);
                string specialHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(specialHashPrep)));

                string finalHash = hashedString + hashedFirstLast + specialHash;
                returnString = finalHash;
            }
            catch (Exception ex)
            {
                returnString = ex.Message;
            }
            return returnString;
        }
    }
}
