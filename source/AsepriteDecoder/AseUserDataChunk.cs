using System.Drawing;

namespace AsepriteDecoder;

public sealed class AseUserDataChunk : AseChunk
{
    public UserDataFlags Flags { get; }
    public string? Text { get; }
    public Color? Color { get; }

    public bool HasText => (Flags & UserDataFlags.HasText) != 0;
    public bool HasColor => (Flags & UserDataFlags.HasColor) != 0;

    internal AseUserDataChunk(uint length, AseBinaryReader reader)
        : base(length, ChunkType.UserData)
    {
        Flags = (UserDataFlags)reader.ReadDword();

        if(HasText)
        {
            Text = reader.ReadString();
        }
        else
        {
            Text = null;
        }

        if(HasColor)
        {
            byte r = reader.ReadByte();
            byte g = reader.ReadByte();
            byte b = reader.ReadByte();
            byte a = reader.ReadByte();

            Color = System.Drawing.Color.FromArgb(a, r, g, b);
        }
        else
        {
            Color = null;
        }
    }
}
