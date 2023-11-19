using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport
{
    //Не реализовал, так как мне лень. Это заглушка
    internal class PasswordHash
    {
        public static string getHash(string pwd)
        {
            return pwd;
        }
        public static bool verifyPassword(string pwd, string hash)
        {
            return pwd == hash;
        }
    }
}
