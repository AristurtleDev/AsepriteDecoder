namespace AsepriteDecoder;

public sealed class AseFrame
{
    public uint Length { get; }
    public ushort MagicNumber { get; }
    public ushort OldChunkCount { get; }
    public ushort Duration { get; }
    public uint NewChunkCount { get; }


    internal AseFrame(AseFile file, AseBinaryReader reader)
    {
        Length = reader.ReadDword();
        MagicNumber = reader.ReadWord();

        if(MagicNumber != 0xF1FA)
        {
            throw new InvalidMagicNumberException("Frame magic number is invalid", MagicNumber);
        }

        OldChunkCount = reader.ReadWord();
        Duration = reader.ReadWord();
        reader.Skip(2);
        NewChunkCount = reader.ReadDword();

        int chunks = GetChunkCount();

        for (int i = 0; i < chunks; i++)
        {

        }
    }

    public int GetChunkCount()
    {
        if(OldChunkCount == 0xFFFF && OldChunkCount < NewChunkCount)
        {
            return (int)NewChunkCount;
        }
        else
        {
            return OldChunkCount;
        }
    }
}
