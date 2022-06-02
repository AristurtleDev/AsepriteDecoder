/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents an Aseprite external file chunk.
    /// </summary>
    public sealed record ExternalFilesChunk
    {
        /// <summary>
        ///     Gets a value that indicates the total number of entries.
        /// </summary>
        public uint Count { get; internal init; }

        /// <summary>
        ///     Gets a <see cref="ExternalFilesEntry"/> array containing the
        ///     records of all external file entries for this chunk.
        /// </summary>
        /// <value></value>
        public ExternalFilesEntry[] ExternalFiles { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="ExternalFilesChunk"/> record
        ///     initialized with the values provided.
        /// </summary>
        /// <param name="count">
        ///     The total number of entries.
        /// </param>
        /// <param name="externalFiles">
        ///     The external file entries.
        /// </param>
        internal ExternalFilesChunk(uint count, ExternalFilesEntry[] externalFiles)
         => (Count, ExternalFiles) = (count, externalFiles);
    }
}