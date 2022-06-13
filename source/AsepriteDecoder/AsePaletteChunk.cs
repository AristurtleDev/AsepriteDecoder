namespace AsepriteDecoder;

public sealed class AsePaletteChunk : AseChunk
{
    public uint NewSize { get; }
    public uint FirstIndex { get; }
    public uint LastIndex { get; }
    public PaletteEntry[] Entries { get; }

    public AsePaletteChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.Palette)
    {
        NewSize = reader.ReadDword();
        FirstIndex = reader.ReadDword();
        LastIndex = reader.ReadDword();

        int total = (int)(LastIndex - FirstIndex + 1);
        Entries = new PaletteEntry[total];

        for (int i = 0; i < total; i++)
        {
            Entries[i] = new PaletteEntry(reader);
        }
    }
}
