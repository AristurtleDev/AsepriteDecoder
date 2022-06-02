/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents the values read from an Aseprite old palette chunk.
    /// </summary>
    public sealed record OldPaletteChunk
    {
        /// <summary>
        ///     Gets a value that indicates the total number of packets.
        /// </summary>
        /// <value></value>
        public ushort Count { get; internal init; }

        /// <summary>
        ///     Gets a <see cref="OldPalettePacket"/> array containing the
        ///     packets for this chunk.
        /// </summary>
        public OldPalettePacket[] Packets { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="OldPaletteChunk"/> record initialized
        ///     with the values provided.
        /// </summary>
        /// <param name="count">
        ///     The total number of packets.
        /// </param>
        /// <param name="packets">
        ///     The packets.
        /// </param>
        public OldPaletteChunk(ushort count, OldPalettePacket[] packets)
            => (Count, Packets) = (count, packets);
    }
}