namespace AsepriteDecoder;

[Flags]
public enum TilesetFlags : uint
{
    ExternalFileLink = 1,
    TilesInsideFile = 2,
    TilemapsUseIDZero = 4
}
