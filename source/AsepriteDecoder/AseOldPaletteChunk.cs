using System.Drawing;

namespace AsepriteDecoder;

public sealed class AseOldPaletteChunk : AseChunk
{
    public ushort PacketCount { get; }
    public OldPalettePacket[] Packets { get; }

    internal AseOldPaletteChunk(uint length, ChunkType type, AseBinaryReader reader)
        : base(length, type)
    {
        PacketCount = reader.ReadWord();

        Packets = new OldPalettePacket[PacketCount];

        for (int i = 0; i < PacketCount; i++)
        {
            Packets[i] = new OldPalettePacket(reader, type == ChunkType.OldPaletteB);
        }
    }
}
