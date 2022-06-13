namespace AsepriteDecoder;

[Flags]
public enum LayerFlags : ushort
{
    Visible = 1,
    Editable = 2,
    LockMovement = 4,
    Background = 8,
    PreferLinkedCels = 16,
    DisplayedCollapsed = 32,
    ReferenceLayer = 64
}
