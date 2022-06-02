/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents an Aseprite user data chunk.
    /// </summary>
    /// <value></value>
    public sealed record UserDataChunk
    {
        /// <summary>
        ///     Gets a value that represents the bitmask flags set for this
        ///     chunk.
        /// </summary>
        public uint Flags { get; internal init; }

        /// <summary>
        ///     Gets a string that contains the text set for the user data.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when <see cref="Flags"/> has bitmask 1
        ///     set; otherwise, this will be <see langword="null"/>
        /// </remarks>        
        public string? Text { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the red value of the color.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when <see cref="Flags"/> has bitmask 2
        ///     set; otherwise, this will be <see langword="null"/>
        /// </remarks>        
        public byte? Red { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the green value of the color.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when <see cref="Flags"/> has bitmask 2
        ///     set; otherwise, this will be <see langword="null"/>
        /// </remarks>        
        public byte? Green { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the blue value of the color.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when <see cref="Flags"/> has bitmask 2
        ///     set; otherwise, this will be <see langword="null"/>
        /// </remarks>
        public byte? Blue { get; internal init; }

        /// <summary>
        ///     Gets a value that represents the alpha value of the color.
        /// </summary>
        /// <remarks>
        ///     This value is only valid when <see cref="Flags"/> has bitmask 2
        ///     set; otherwise, this will be <see langword="null"/>
        /// </remarks>
        public byte? Alpha { get; internal init; }


        /// <summary>
        ///     Creates a new <see cref="UserDataChunk"/> record initialized
        ///     with the values provided.
        /// </summary>
        /// <param name="flags">
        ///     The bitmask flags for this chunk.
        /// </param>
        internal UserDataChunk(uint flags) => Flags = flags;
    }
}