namespace AsepriteDecoder;

public sealed class AseSliceChunk : AseChunk
{
    public uint NumberOfKeys { get; }
    public SliceFlags Flags { get; }
    public string Name { get; }
    public SliceKey[] Keys { get; }

    public bool IsNinePatch => (Flags & SliceFlags.IsNinePatch) != 0;
    public bool HasPivot => (Flags & SliceFlags.HasPivot) != 0;

    internal AseSliceChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.Slice)
    {
        NumberOfKeys = reader.ReadDword();
        Flags = (SliceFlags)reader.ReadDword();
        reader.Skip(4);
        Name = reader.ReadString();

        Keys = new SliceKey[NumberOfKeys];

        for (int i = 0; i < NumberOfKeys; i++)
        {
            Keys[i] = new SliceKey(IsNinePatch, HasPivot, reader);
        }
    }
}
