using System;
using System.IO;
using System.IO.Compression;

namespace RDAExplorer.ZLib
{
    public class ZLib
    {
        public static byte[] Uncompress(byte[] input, int uncompressedSize)
        {
            using (MemoryStream inputStream = new MemoryStream(input))
            {
                using (ZLibStream zlibStream = new ZLibStream(inputStream, CompressionMode.Decompress, leaveOpen: false))
                {
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        zlibStream.CopyTo(outputStream);
                        byte[] result = outputStream.ToArray();
                        Array.Resize(ref result, uncompressedSize);
                        return result;
                    }
                }
            }
        }

        public static byte[] Compress(byte[] input)
        {
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (ZLibStream zlibStream = new ZLibStream(outputStream, CompressionLevel.Fastest, leaveOpen: true))
                {
                    zlibStream.Write(input, 0, input.Length);
                }

                return outputStream.ToArray();
            }
        }
    }
}
