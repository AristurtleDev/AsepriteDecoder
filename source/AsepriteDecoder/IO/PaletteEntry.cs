/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents an entry in a <see cref="PaletteChunk"/>.
    /// </summary>
    public sealed record PaletteEntry
    {
        /// <summary>
        ///     Gets a value that indicates the bitmask flags set for this
        ///     entry.
        /// </summary>
        public ushort Flags { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the red value of the color.
        /// </summary>
        public byte Red { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the green value of the color.
        /// </summary>
        public byte Green { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the blue value of the color.
        /// </summary>
        public byte Blue { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the alpha value of the color.
        /// </summary>
        public byte Alpha { get; internal init; }

        /// <summary>
        ///     Gets a string that contains the name of this color.
        /// </summary>
        /// <remarks>
        ///     This will only contain a valid value when the
        ///     <see cref="Flags"/> bitmask 1 is set; otherwise, this will be
        ///     <see langword=""="null"/>
        /// </remarks>
        public string? Name { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="PaletteEntry"/> record initialized with
        ///     the values provided.
        /// </summary>
        /// <param name="flags">
        ///     The bitmask flags for this entry.
        /// </param>
        /// <param name="red">
        ///     The red value of the palette color.
        /// </param>
        /// <param name="green">
        ///     The green value of the palette color.
        /// </param>
        /// <param name="blue">
        ///     The blue value of the palette color.
        /// </param>
        /// <param name="alpha">
        ///     The alpha value of the palette color.
        /// </param>
        internal PaletteEntry(ushort flags, byte red, byte green, byte blue, byte alpha)
         => (Flags, Red, Green, Blue, Alpha) = (flags, red, green, blue, alpha);
    }
}