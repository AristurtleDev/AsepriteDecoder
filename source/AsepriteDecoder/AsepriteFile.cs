using System.Drawing;

namespace AsepriteDecoder
{
    public sealed record AsepriteFile
    {
        public Frame[] Frames { get; internal init; }
        public Tag[]? Tas { get; internal init; }
        public Slice[]? Slices { get; internal init; }
        public Dictionary<int, string>? ExternalFiles { get; internal init; }
        public Dictionary<int, Tileset>? Tilesets { get; internal init; }


        internal AsepriteFile(Frame[] frames)
            => (Frames) = (frames);
    }
}
