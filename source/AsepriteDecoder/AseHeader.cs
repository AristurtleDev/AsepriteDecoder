namespace AsepriteDecoder;

public sealed record AseHeader
{
    public uint FileSize { get; }
    public ushort MagicNumber { get; }
    public ushort FrameCount { get; }
    public ushort FrameWidth { get; }
    public ushort FrameHeight { get; }
    public ColorDepth ColorDepth { get; }
    public HeaderFlags Flags { get; }
    public ushort Speed { get; }
    public byte TransparentIndex { get; }
    public ushort NumberOfColors { get; }
    public byte PixelWidth { get; }
    public byte PixelHeight { get; }
    public short GridX { get; }
    public short GridY { get; }
    public ushort GridWidth { get; }
    public ushort GridHeight { get; }



    internal AseHeader(AseBinaryReader reader)
    {
        FileSize = reader.ReadDword();
        MagicNumber = reader.ReadWord();

        if(MagicNumber != 0xA5E0)
        {
            throw new InvalidMagicNumberException("Header magic number is invalid", MagicNumber);
        }

        FrameCount = reader.ReadWord();
        FrameWidth = reader.ReadWord();
        FrameHeight = reader.ReadWord();
        ColorDepth = (ColorDepth)reader.ReadWord();
        Flags = (HeaderFlags)reader.ReadDword();
        Speed = reader.ReadWord();
        reader.Skip(8);
        TransparentIndex = reader.ReadByte();
        reader.Skip(3);
        NumberOfColors = reader.ReadWord();
        PixelWidth = reader.ReadByte();
        PixelHeight = reader.ReadByte();
        GridX = reader.ReadShort();
        GridY = reader.ReadShort();
        GridWidth = reader.ReadWord();
        GridHeight = reader.ReadWord();
    }
}
