/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents a packet values for an <see cref="OldPaletteChunk"/>
    ///     record.
    /// </summary>
    public sealed record OldPalettePacket
    {
        /// <summary>
        ///     Gets a value that indicates the number of palette entries to 
        ///     skip from the last packet (start from 0).
        /// </summary>
        public byte NumberToSkip { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the total number of colors in this
        ///     packet.
        /// </summary>
        /// <remarks>
        ///     Per Aseprite file spec, 0 means 256.
        /// </remarks>
        public byte NumberOfColors { get; internal init; }

        /// <summary>
        ///     Gets a <see cref="OldPaletteColor"/> array containing palette
        ///     colors for this packet.
        /// </summary>
        public OldPaletteColor[] Colors { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="OldPalettePacket"/> record initialized
        ///     with the values provided.
        /// </summary>
        /// <param name="toSkip">
        ///     The total number of palette entries to skip from the last
        ///     packet.
        /// </param>
        /// <param name="numColors">
        ///     The total number of colors in this packet.
        /// </param>
        /// <param name="colors">
        ///     The colors for this packet.
        /// </param>
        public OldPalettePacket(byte toSkip, byte numColors, OldPaletteColor[] colors)
         => (NumberToSkip, NumberOfColors, Colors) = (toSkip, numColors, colors);
    }
}