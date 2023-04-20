using System.Text;

public class Error
{
    public enum ErrorType
    {
        IllegalCharacter,
        InvalidSyntax,
        RuntimeError,
    }

    public readonly ErrorType Type;
    public readonly string Details;
    public readonly Position? Start;
    public readonly Position? End;
    public readonly Context? Context;

    public Error(ErrorType errorType, string details, Position? start, Position? end, Context? context = null)
    {
        Type = errorType;
        Details = details;
        Start = start;
        End = end;
        Context = context;
    }

    public override string ToString()
    {
        if (Start is null || End is null || Context is null)
        {
            return $"{Type}: {Details}";
        }

        var tracebackString = GetTracebackString();
        return tracebackString;
    }
    
    private string GetPositionString()
    {
        // var result = new StringBuilder();
        // TODO:
        // This doesn't show the full error (it cuts of right parens)
        // because the Node needs to store a start and end
        return Start!.FileText.Substring(Start!.Index, End!.Index);
    }
    
    private string GetTracebackString()
    {
        var result = "";
        var tracePosition = Start!;
        var traceContext = Context;

        while (traceContext is not null)
        {
            var locationString = $"File {tracePosition!.FileName}, Line {tracePosition.Line + 1}, In {traceContext.DisplayName}\n{result}";
            result = locationString;
            tracePosition = traceContext.ParentEntryPosition;
            traceContext = traceContext.Parent;
        }

        return $"Traceback (most recent call last):\n{result}\n{Type}\n{GetPositionString()}";
    }
}