public class Context
{
    public readonly string DisplayName;
    public readonly Context? Parent;
    public readonly Position? ParentEntryPosition;

    public Context(string displayName, Context? parent = null, Position? parentEntryPosition = null)
    {
        DisplayName = displayName;
        Parent = parent;
        ParentEntryPosition = parentEntryPosition;
    }
}