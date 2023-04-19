public class Error
{
    public enum ErrorType
    {
        IllegalCharacter,
    }

    private ErrorType _errorType;
    private string _details;
    private Position _start;
    private Position _end;

    public Error(ErrorType errorType, Position start, Position end, string details)
    {
        _errorType = errorType;
        _details = details;
        _start = start;
        _end = end;
    }

    public override string ToString()
    {
        return $"{_errorType}: {_details}\nFile {_start.FileName}, Line {_start.Line + 1}";
    }
}