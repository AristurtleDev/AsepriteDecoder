namespace AsepriteDecoder;

public sealed class AseColorProfileChunk : AseChunk
{
    public ColorProfileType Type { get; }
    public ColorProfileFlags Flags { get; }
    public float FixedGamma { get; }
    public uint? ICCDataLength { get; }
    public byte[]? ICCData { get; }

    public AseColorProfileChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.ColorProfile)
    {
        Type = (ColorProfileType)reader.ReadWord();
        Flags = (ColorProfileFlags)reader.ReadWord();
        FixedGamma = reader.ReadFixed();
        reader.Skip(8);

        ICCDataLength = null;
        ICCData = null;

        if (Type == ColorProfileType.EmbeddedICC)
        {
            ICCDataLength = reader.ReadDword();
            ICCData = reader.ReadBytes((int)ICCDataLength);
        }
    }
}
