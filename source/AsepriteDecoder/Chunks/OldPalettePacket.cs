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

using System.Collections;
using System.Drawing;

namespace AsepriteDecoder.Chunks;

/// <summary>
///     Represents an packet for an old palette chunk in an Aseprite file.
/// </summary>
public sealed class OldPalettePacket : IEnumerable<Color>, IEnumerable
{
    /// <summary>
    ///     Gets the total number of entries to skip from the last packet.
    /// </summary>
    public byte NumberToSkip { get; }

    /// <summary>
    ///     Gets the total number of colors in this packet.
    /// </summary>
    public byte NumberOfColors { get; }

    /// <summary>
    ///     Gets the array of <see cref="Color"/> values included in this
    ///     packet.
    /// </summary>
    public Color[] Colors { get; }

    /// <summary>
    ///     Gets the <see cref="Color"/> value at the index provided.
    /// </summary>
    public Color this[int index]
    {
        get
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "The index must be greater than or equal to zero");
            }

            if (index >= Colors.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "The index value cannot excceed the total number of entries");
            }

            return Colors[index];
        }
    }

    /// <summary>
    ///     Creates a new <see cref="OldPalettePacket"/> class instance
    ///     initialized by the <see cref="AseBinaryReader"/> class instance
    ///     provided.
    /// </summary>
    /// <param name="reader">
    ///     The <see cref="AseBinaryReader"/> class instance used to initialize
    ///     this instance.
    /// </param>
    /// <param name="isTypeB">
    ///     <see langword="true"/> if this is for a chunk that is
    ///     <see cref="ChunkType.OldPaletteB"/>; otherwise,
    ///     <see langword="false"/>.
    /// </param>
    internal OldPalettePacket(AseBinaryReader reader, bool isTypeB)
    {
        NumberToSkip = reader.ReadByte();
        NumberOfColors = reader.ReadByte();

        Colors = new Color[NumberOfColors];

        for (int i = 0; i < NumberOfColors; i++)
        {
            byte r = reader.ReadByte();
            byte g = reader.ReadByte();
            byte b = reader.ReadByte();

            if (isTypeB)
            {
                //  Type B uses 6-bits per color value, so we
                //  need to convert each value to 8-bit (0 - 255)
                r = (byte)((r << 2) | (r >> 4));
                g = (byte)((g << 2) | (g >> 4));
                b = (byte)((b << 2) | (b >> 4));
            }

            Colors[i] = Color.FromArgb(255, r, g, b);
        }
    }

    /// <summary>
    ///     Returns an enumerator that iterates through the <see cref="Color"/>
    ///     values included in this packet.
    /// </summary>
    /// <returns>
    ///     An <see cref="IEnumerator{T}"/> for the <see cref="Color"/> values
    ///     included in this packet.
    /// </returns>
    public IEnumerator<Color> GetEnumerator()
    {
        for (int i = 0; i < Colors.Length; i++)
        {
            yield return Colors[i];
        }
    }

    /// <summary>
    ///     Returns an enumerator that iterates through the <see cref="Color"/>
    ///     values included in this packet.
    /// </summary>
    /// <returns>
    ///     An <see cref="IEnumerator{T}"/> for the <see cref="Color"/> values
    ///     included in this packet.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
