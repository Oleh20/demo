public enum TargetType
{
    Good,
    Bad
}

public class TargetModel
{
    public TargetType Type { get; private set; }

    public TargetModel(TargetType type)
    {
        Type = type;
    }
}
