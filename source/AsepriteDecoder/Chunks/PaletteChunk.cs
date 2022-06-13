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
using System.Collections.ObjectModel;

namespace AsepriteDecoder.Chunks;

/// <summary>
///     Represents a palette chunk in an Aseprite file.
/// </summary>
public sealed class AsePaletteChunk : AseChunk, IEnumerable<PaletteEntry>, IEnumerable
{
    //  The collection of all palette entries added to this chunk.
    private List<PaletteEntry> _entries;

    /// <summary>
    ///     Gets the new palette size that should be set when this chunk is
    ///     read.
    /// </summary>
    public uint NewSize { get; }

    /// <summary>
    ///     Gets the first color index within the palette to change.
    /// </summary>
    public uint FirstIndex { get; }

    /// <summary>
    ///     Gets the last color index within the palette to change.
    /// </summary>
    public uint LastIndex { get; }

    /// <summary>
    ///     Gets a read only collection of all <see cref="PaletteEntry"/>
    ///     instnaces that are part of this palette chunk.
    /// </summary>
    public ReadOnlyCollection<PaletteEntry> Entries { get; }

    /// <summary>
    ///     Gets the <see cref="PaletteEntry"/> at the index provided.
    /// </summary>
    public PaletteEntry this[int index]
    {
        get
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "The index must be greater than or equal to zero");
            }

            if (index >= _entries.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "The index value cannot excceed the total number of entries");
            }

            return _entries[index];
        }
    }

    /// <summary>
    ///     Creates a new <see cref="AsePaletteChunk"/> class instance
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
    public AsePaletteChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.Palette)
    {
        NewSize = reader.ReadDword();
        FirstIndex = reader.ReadDword();
        LastIndex = reader.ReadDword();

        int total = (int)(LastIndex - FirstIndex + 1);

        _entries = new List<PaletteEntry>(total);
        Entries = _entries.AsReadOnly();

        for (int i = 0; i < total; i++)
        {
            _entries.Add(new PaletteEntry(reader));
        }
    }

    /// <summary>
    ///     Returns an enumerator that iterates through the collection of
    ///     <see cref="PaletteEntry"/> instances that make up this palette
    ///     chunk.
    /// </summary>
    /// <returns>
    ///     A <see cref="IEnumerator{T}"/> for the collection of
    ///     <see cref="PaletteEntry"/> instances that make up this palette
    ///     chunk.
    /// </returns>
    public IEnumerator<PaletteEntry> GetEnumerator() => Entries.GetEnumerator();

    /// <summary>
    ///     Returns an enumerator that iterates through the collection of
    ///     <see cref="PaletteEntry"/> instances that make up this palette
    ///     chunk.
    /// </summary>
    /// <returns>
    ///     A <see cref="IEnumerator{T}"/> for the collection of
    ///     <see cref="PaletteEntry"/> instances that make up this palette
    ///     chunk.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
