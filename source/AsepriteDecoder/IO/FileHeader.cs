/* -----------------------------------------------------------------------------
    Aseprite Decoder
    Copyright (c) 2022 Christopher Whitley
    This file is licensed under the MIT license. Read the LICENSE file for
    more information
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.IO
{
    /// <summary>
    ///     Represents the values read from an Aseprite file header.
    /// </summary>
    public sealed record FileHeader
    {
        /// <summary>
        ///     Gets a value that indicates the total size, in bytes, of the
        ///     Aseprite file
        /// </summary>
        public uint FileSize { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the file header magic number used
        ///     to validate the file.
        /// </summary>
        public ushort MagicNumber { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the total number of frames.
        /// </summary>
        public ushort FrameCount { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates thew width, in pixels, of each
        ///     frame.
        /// </summary>
        public ushort FrameWidth { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the height, in pixels, of each
        ///     frame.
        /// </summary>
        public ushort FrameHeight { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the color depth, or bits per pixel,
        ///     used.
        /// </summary>
        public ushort ColorDepth { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the bitmask flags set for the file
        ///     header.
        /// </summary>
        public uint Flags { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the duration, in milliseconds,
        ///     between each frame.
        /// </summary>
        /// <remarks>
        ///     Per Aseprite file spec, this value is deprecated, users should 
        ///     instead use the duration value of each individual frame.
        /// </remarks>
        public ushort Speed { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the index of hte color in the
        ///     palette that represents a transparent pixel.
        /// </summary>
        /// <remarks>
        ///     This value is only valid if the <see cref="ColorDepth"/> value
        ///     is 8 (Indexed)
        /// </remarks>
        public byte TransparentIndex { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the total number of colors.
        /// </summary>
        public ushort NumberOfColors { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the width of a pixel.
        /// </summary>
        public byte PixelWidth { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the height of a pixel.
        /// </summary>
        public byte PixelHeight { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the top-left x-coordinate position
        ///     of the grid.
        /// </summary>
        public short GridX { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the top-left y-coordinate position
        ///     of the grid.
        /// </summary>
        public short GridY { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the height, in pixels, of the grid.
        /// </summary>
        public ushort GridHeight { get; internal init; }

        /// <summary>
        ///     Gets a value that indicates the width, in pixels, of the grid.
        /// </summary>
        public ushort GridWidth { get; internal init; }
    }
}