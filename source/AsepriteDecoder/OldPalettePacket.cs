using System.Drawing;

namespace AsepriteDecoder;

public sealed class OldPalettePacket
{
    public byte NumberToSkip { get; }
    public byte NumberOfColors { get; }
    public Color[] Colors { get; }

    internal OldPalettePacket(AseBinaryReader reader, bool isTypeB)
    {
        NumberToSkip = reader.ReadByte();
        NumberOfColors = reader.ReadByte();

        Colors = new Color[NumberOfColors];

        for (int i = 0; i < NumberOfColors; i++)
        {
            byte r = reader.ReadByte();
            byte g = reader.ReadByte();
            byte b = reader.ReadByte();

            if (isTypeB)
            {
                //  Type B uses 6-bits per color value, so we
                //  need to convert each value to 8-bit (0 - 255)
                r = (byte)((r << 2) | (r >> 4));
                g = (byte)((g << 2) | (g >> 4));
                b = (byte)((b << 2) | (b >> 4));
            }

            Colors[i] = Color.FromArgb(255, r, g, b);
        }
    }
}
