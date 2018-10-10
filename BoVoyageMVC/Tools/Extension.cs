using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace BoVoyageMVC.Tools
{
    public static class Extension
    {
        public static string HashMD(this string value)//on besoin du this pour declarer un méthode d'extention
        {
            byte[] valueBytes = System.Text.Encoding.Default.GetBytes(value);//tableaux des bytes

            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] calcul = provider.ComputeHash(valueBytes);

            string result = "";
            foreach(byte b in calcul)
            {
                if (b < 16)
                    result += "0" + b.ToString("x");
                else
                    result+= b.ToString("x");
            }
            return result;
        }

        
    }
}