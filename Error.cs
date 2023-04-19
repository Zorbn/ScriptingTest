public class Error
{
    public enum ErrorType
    {
        IllegalCharacter,
        InvalidSyntax,
    }

    public readonly ErrorType Type;
    public readonly string Details;
    public readonly Position? Start;
    public readonly Position? End;

    public Error(ErrorType errorType, Position? start, Position? end, string details)
    {
        Type = errorType;
        Details = details;
        Start = start;
        End = end;
    }

    public override string ToString()
    {
        if (Start is null || End is null)
        {
            return $"{Type}: {Details}";
        }

        return $"{Type}: {Details}\nFile {Start.FileName}, Line {Start.Line + 1}";
    }
}