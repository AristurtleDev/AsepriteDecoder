using System.Runtime.Serialization;

namespace AsepriteDecoder;

[Serializable]
public sealed class UnknownChunkTypeException : Exception, ISerializable
{
    public ushort? ChunkType { get; set; }

    public UnknownChunkTypeException(string message, ushort chunkType)
        : base(message) => ChunkType = chunkType;

    public UnknownChunkTypeException(SerializationInfo info, StreamingContext context)
    {
        ChunkType = (ushort?)info.GetValue("chunkType", typeof(ushort));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("chunkType", ChunkType, typeof(ushort));
    }
}
