using System;
using System.Text.RegularExpressions;

namespace OOP
{
    class EMailAdress
    {
        private string adress;

        public EMailAdress(string adress)
        {
            this.adress = adress;
            if (!isValid(adress))
                throw new Exception("Not a valid E-Mail-Adress!");
        }

        public static bool isValid(string adress)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(adress);
            return match.Success;
        }
    }
}
