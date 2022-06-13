namespace AsepriteDecoder;

public class AseChunk
{
    public uint Length { get; }
    public ChunkType ChunkType { get; }

    internal AseChunk(uint length, ChunkType type) =>
        (Length, ChunkType) = (length, type);

    internal static AseChunk ReadChunk(AseBinaryReader reader)
    {
        uint len = reader.ReadDword();
        ChunkType type = (ChunkType)reader.ReadWord();

        AseChunk chunk = type switch
        {
            ChunkType.OldPaletteA => new AseOldPaletteChunk(len, type, reader),
            ChunkType.OldPaletteB => new AseOldPaletteChunk(len, type, reader),
            ChunkType.Layer => new AseLayerChunk(len, reader),
            ChunkType.Cel => new AseCelChunk(len, reader),
            ChunkType.CelExtra => new AseCelExtraChunk(len, reader),
            ChunkType.ColorProfile => new AseColorProfileChunk(len, reader),
            ChunkType.ExternalFiles => new AseExternalFilesChunk(len, reader),
            ChunkType.Mask => new AseMaskChunk(len, reader),
            ChunkType.Path => throw new NotImplementedException(),
            ChunkType.Tags => new AseTagsChunk(len, reader),
            ChunkType.Palette => new AsePaletteChunk(len, reader),
            ChunkType.UserData => new AseUserDataChunk(len, reader),
            ChunkType.Slice => new AseSliceChunk(len, reader),
            ChunkType.Tileset => new AseTilesetChunk(len, reader),
            _ => throw new UnknownChunkTypeException("The chunk type found is unknown", type)
        };
    }
}
