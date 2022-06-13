namespace AsepriteDecoder;

public sealed class AseTagsChunk : AseChunk
{
    public ushort NumberOfTags { get; }
    public Tag[] Tags { get; }

    internal AseTagsChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.Tags)
    {
        NumberOfTags = reader.ReadWord();
        reader.Skip(8);

        Tags = new Tag[NumberOfTags];

        for (int i = 0; i < NumberOfTags; i++)
        {
            Tags[i] = new Tag(reader);
        }
    }
}
