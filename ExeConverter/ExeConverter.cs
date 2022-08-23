using ExeConverter.Properties;
using System;
using System.Linq;
using System.Text;

namespace ExeConverter
{
    public static class ExeConverter
    {
        public static string ExeToBat(byte[] exe)
        {
            string[] b64 = Convert.ToBase64String(exe)
                .Select( (c, index) => new { c, index })
                .GroupBy(x => x.index / 70)
                .Select(group => group.Select(elem => elem.c) )
                .Select(chars => new string(chars.ToArray() ) )
                .ToArray();

            StringBuilder echos = new StringBuilder();

            foreach (var line in b64.Select( (value, i) => new { i, value }) )
            {
                echos.Append("echo ");
                echos.Append(line.value);
                
                if (line.i == 0) echos.Append(">%tmp%\\x");
                else echos.Append(">>%tmp%\\x");

                if (line.i != b64.Length - 1) echos.AppendLine();
            }

            return Resources.exe2bat.Replace("#echos#", echos.ToString() );
        }

        public static byte[] ExeToBatNonPrintable(byte[] exe)
        {
            return CabMaker.MakeCab(exe);
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
