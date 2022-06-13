using System.Runtime.Serialization;

namespace AsepriteDecoder;

[Serializable]
public class InvalidMagicNumberException : Exception, ISerializable
{
    public ushort? MagicNumber { get; set; }

    public InvalidMagicNumberException(string message, ushort magic)
        : base(message) => MagicNumber = magic;

    public InvalidMagicNumberException(SerializationInfo info, StreamingContext context)
    {
        MagicNumber = (ushort?)info.GetValue("magic", typeof(ushort));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue("magic", MagicNumber, typeof(ushort));
    }
}
