using ExeConverter.Properties;
using System;

namespace ExeConverter
{
    public static class ExeConverter
    {
        public static string ExeToBat(byte[] exe)
        {
            return Resources.exe2bat.Replace("#base64#", Convert.ToBase64String(exe) );
        }

        public static string ExeToVbs(byte[] exe)
        {
            return Resources.exe2vbs.Replace("#base64#", Convert.ToBase64String(exe) );
        }

        public static string ExeToJs(byte[] exe)
        {
            return Resources.exe2js.Replace("#base64#", Convert.ToBase64String(exe) );
        }

        public static string ExeToPs1(byte[] exe)
        {
            return Resources.exe2ps1.Replace("#base64#", Convert.ToBase64String(exe) );
        }
    }
}
