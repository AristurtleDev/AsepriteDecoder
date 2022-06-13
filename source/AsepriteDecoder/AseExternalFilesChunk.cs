namespace AsepriteDecoder;

public sealed class AseExternalFilesChunk : AseChunk
{
    public uint NumberOfEntries { get; }
    public ExternalFileEntry[] Entries { get; }

    internal AseExternalFilesChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.ExternalFiles)
    {
        NumberOfEntries = reader.ReadDword();
        reader.Skip(8);

        Entries = new ExternalFileEntry[NumberOfEntries];

        for (int i = 0; i < NumberOfEntries; i++)
        {
            Entries[i] = new ExternalFileEntry(reader);
        }
    }
}
