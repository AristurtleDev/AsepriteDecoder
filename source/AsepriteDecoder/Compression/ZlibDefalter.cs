/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

using System.IO.Compression;

namespace AsepriteDecoder.Compression
{
    /// <summary>
    ///     Utility class used to deflate a Zlib compressed data buffer.
    /// </summary>
    internal static class ZlibDeflater
    {
        /// <summary>
        ///     Deflates a byte array that has been compressed with Zlib
        /// </summary>
        /// <param name="buffer">
        ///     The array of compressed bytes.
        /// </param>
        /// <returns>
        ///     A byte array containing the decompressed buffer.
        /// </returns>
        public static byte[] Deflate(byte[] buffer)
        {
            using (MemoryStream compressedStream = new MemoryStream(buffer))
            {
                //  The first two bytes of the compressed stream is zlib header
                //  information that we don't need.  We can just ignore and
                //  advances the stream past it
                _ = compressedStream.ReadByte();
                _ = compressedStream.ReadByte();

                //  Now we can deflate
                using (MemoryStream decompressedStream = new MemoryStream())
                {
                    using (DeflateStream deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
                    {
                        deflateStream.CopyTo(decompressedStream);
                        return decompressedStream.ToArray();
                    }
                }
            }
        }
    }
}