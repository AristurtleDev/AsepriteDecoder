namespace AsepriteDecoder
{
    public sealed record Slice
    {
        public bool IsNinePatch { get; internal init; }
        public bool HasPivot { get; internal init; }
        public string Name { get; internal init; }
        public SliceKey[] Keys { get; internal init; }

        internal Slice(bool isNinePatch, bool hasPivot, string name, SliceKey[] keys)
            => (IsNinePatch, HasPivot, Name, Keys) = (isNinePatch, hasPivot, name, keys);
    }
}
