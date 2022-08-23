using System;
using System.IO;
using System.Linq;

namespace ExeConverter
{
    public static class CabMaker
    {
        public static byte[] MakeCab(byte[] filebytes)
        {
            byte[][] fileparts = filebytes
                .Select( (c, index) => new { c, index })
                .GroupBy(x => x.index / 32767)
                .Select(group => group.Select(elem => elem.c).ToArray() )
                .ToArray();

            using (MemoryStream ms = new MemoryStream() )
            using (BinaryWriter bw = new BinaryWriter(ms) )
            {
                bw.Write("MSCF".ToCharArray() );
                bw.Write(0u);
                bw.Write(uint.MaxValue); // cbCabinet
                bw.Write(0u);
                bw.Write(44u + 73u); // coffFiles
                bw.Write(0u);
                bw.Write( (byte) 3);
                bw.Write( (byte) 1);
                bw.Write( (ushort) 1u);
                bw.Write( (ushort) 1u);
                bw.Write( (ushort) 0u);
                bw.Write( (ushort) 0u);
                bw.Write( (ushort) 0u);

                bw.Write(66u + 73u); // coffCabStart
                bw.Write( (ushort) fileparts.Length); // cCFData (count of CFDATA)
                bw.Write( (ushort) 0u); // typeCompress

                bw.Write("\n\r\n\rcls && extrac32 /y \"%~f0\" \"%tmp%\\x.exe\" && start \"\" \"%tmp%\\x.exe\"\n\r\n\r".ToCharArray() );

                bw.Write( (uint) filebytes.Length);
                bw.Write(0u);
                bw.Write( (ushort) 0u);
                bw.Write( (byte) 1);
                bw.Write( (byte) 1);
                bw.Write( (byte) 0);
                bw.Write( (byte) 0);
                bw.Write( (ushort) 32u);
                bw.Write("x.exe\0".ToCharArray() );

                foreach (byte[] part in fileparts)
                {
                    bw.Write(0u);
                    bw.Write( (ushort) part.Length);
                    bw.Write( (ushort) part.Length);
                    bw.Write(part);
                }

                bw.Flush();
                return ms.ToArray();
            }
        }
    }
}
