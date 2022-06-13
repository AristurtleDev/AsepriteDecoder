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

using System.Collections.ObjectModel;
using System.Collections;

namespace AsepriteDecoder.Chunks;

/// <summary>
///     Represents an old palette chunk in an Aseprite file.
/// </summary>
public sealed class AseOldPaletteChunk : AseChunk, IEnumerable<OldPalettePacket>, IEnumerable
{
    //  The collection of all packets added.
    private List<OldPalettePacket> _packets;
    /// <summary>
    ///     Gets the total number of packets in this chunk.
    /// </summary>
    public ushort PacketCount { get; }

    /// <summary>
    ///     Gets a read only collection of all <see cref="OldPalettePacket"/>
    ///     instances add to this chunk.
    /// </summary>
    public ReadOnlyCollection<OldPalettePacket> Packets { get; }

    /// <summary>
    ///     Gets the <see cref="OldPalettePacket"/> at the index provided.
    /// </summary>
    public OldPalettePacket this[int index]
    {
        get
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "The index must be greater than or equal to zero");
            }

            if (index >= _packets.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "The index value cannot excceed the total number of entries");
            }

            return _packets[index];
        }
    }

    /// <summary>
    ///     Creates a new <see cref="AseOldPaletteChunk"/> class instance
    ///     initialized by the <see cref="AseBinaryReader"/> class instance
    ///     provided.
    /// </summary>
    /// <param name="length">
    ///     The total length, in bytes, of the chunk.
    /// </param>
    /// <param name="isTypeB">
    ///     <see langword="true"/> if this is a
    ///     <see cref="ChunkType.OldPaletteB"/> type chunk; otherwise,
    ///     <see langword="false"/>.
    /// </param>
    /// <param name="reader">
    ///     The <see cref="AseBinaryReader"/> class instance used to initialize
    ///     this instance.
    /// </param>
    internal AseOldPaletteChunk(uint length, bool isTypeB, AseBinaryReader reader)
        : base(length, isTypeB ? ChunkType.OldPaletteB : ChunkType.OldPaletteA)
    {
        PacketCount = reader.ReadWord();

        _packets = new List<OldPalettePacket>(PacketCount);
        Packets = _packets.AsReadOnly();

        for (int i = 0; i < PacketCount; i++)
        {
            _packets.Add(new OldPalettePacket(reader, isTypeB));
        }
    }

    /// <summary>
    ///     Returns an enumerator that iterates through the
    ///     <see cref="Packets"/> collection.
    /// </summary>
    /// <returns>
    ///     An <see cref="IEnumerator{T}"/> for the <see cref="Packets"/>
    ///     collection.
    /// </returns>
    public IEnumerator<OldPalettePacket> GetEnumerator() => Packets.GetEnumerator();

    /// <summary>
    ///     Returns an enumerator that iterates through the
    ///     <see cref="Packets"/> collection.
    /// </summary>
    /// <returns>
    ///     An <see cref="IEnumerator{T}"/> for the <see cref="Packets"/>
    ///     collection.
    /// </returns>F
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


}
