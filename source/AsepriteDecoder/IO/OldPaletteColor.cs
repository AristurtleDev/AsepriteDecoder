/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents the color values for an <see cref="OldPalettePacket"/>.
    /// </summary>
    public sealed record OldPaletteColor
    {
        /// <summary>
        ///     Gets a value that indicates the red level of this color.
        /// </summary>
        public byte Red { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the green level of this color.
        /// </summary>
        public byte Green { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the blue level of this color.
        /// </summary>
        public byte Blue { get; internal init; }

        /// <summary>
        ///     Creates a new <see cref="OldPaletteColor"/> record initialized
        ///     with the values provided.
        /// </summary>
        /// <param name="red">
        ///     The red value.
        /// </param>
        /// <param name="green">
        ///     The green value.
        /// </param>
        /// <param name="blue">
        ///     The blue value.
        /// </param>
        internal OldPaletteColor(byte red, byte green, byte blue)
            => (Red, Green, Blue) = (red, green, blue);
    }
}