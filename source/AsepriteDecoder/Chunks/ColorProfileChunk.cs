/* -----------------------------------------------------------------------------
    Copyright 2022 Christopher Whitley

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to
    deal in the Software without restriction, including without limitation the
    rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
    sell copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
    FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
    IN THE SOFTWARE.
----------------------------------------------------------------------------- */

namespace AsepriteDecoder.Chunks;

/// <summary>
///     Represents a Color Profile chunk from an Aseprite file.
/// </summary>
public sealed class ColorProfileChunk : AseChunk
{
    /// <summary>
    ///     Gets the type of color profile this is.
    /// </summary>
    public ColorProfileType Type { get; }

    /// <summary>
    ///     Gets the bitmask flags set for this chunk.
    /// </summary>
    public ColorProfileFlags Flags { get; }

    /// <summary>
    ///     Gets the fixed gamma value used.
    /// </summary>
    public float FixedGamma { get; }

    /// <summary>
    ///     Gets the length, in bytes, of the <see cref="ICCData"/> array.
    /// </summary>
    /// <remarks>
    ///     This is only valid when the <see cref="Type"/> is
    ///     <see cref="ColorProfileType.EmbeddedICC"/>; otherwise, this will
    ///     be <see langword="null"/>
    /// </remarks>
    public uint? ICCDataLength { get; }

    /// <summary>
    ///     Gets the embedded ICC profile data if it was provided.
    /// </summary>
    /// <remarks>
    ///     This is only valid when the <see cref="Type"/> is
    ///     <see cref="ColorProfileType.EmbeddedICC"/>; otherwise, this will
    ///     be <see langword="null"/>
    /// </remarks>
    public byte[]? ICCData { get; }

    /// <summary>
    ///     Gets whether a special fixed gamma value was used.
    /// </summary>
    /// <returns>
    ///     <see langword="true"/> if a special fixed gamma was used; otherwise,
    ///     <see langword="false"/>.
    /// </returns>
    public bool UsesSpecialFixedGamma => (Flags & ColorProfileFlags.UseSpecialFixedGamma) != 0;

    /// <summary>
    ///     Creates a new <see cref="ColorProfileChunk"/> class instance
    ///     initialized by the <see cref="AseBinaryReader"/> class instance
    ///     provided.
    /// </summary>
    /// <param name="length">
    ///     The total length, in bytes, of the chunk.
    /// </param>
    /// <param name="reader">
    ///     The <see cref="AseBinaryReader"/> class instance used to initialize
    ///     this instance.
    /// </param>
    public ColorProfileChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.ColorProfile)
    {
        Type = (ColorProfileType)reader.ReadWord();
        Flags = (ColorProfileFlags)reader.ReadWord();
        FixedGamma = reader.ReadFixed();
        reader.Skip(8);

        if (Type == ColorProfileType.EmbeddedICC)
        {
            ICCDataLength = reader.ReadDword();
            ICCData = reader.ReadBytes((int)ICCDataLength);
        }
        else
        {
            ICCDataLength = null;
            ICCData = null;
        }
    }
}
