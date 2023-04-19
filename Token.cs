public class Token
{
    public enum TokenType
    {
        Integer,
        Float,
        Plus,
        Minus,
        Multiply,
        Divide,
        LeftParen,
        RightParen,
        EOF,
    }

    public readonly TokenType Type;
    public readonly dynamic? Value; // TODO: Figure out alternative to dynamic.
    public readonly Position? Start;
    public readonly Position? End;

    public Token(TokenType type, dynamic? value = null, Position? start = null, Position? end = null)
    {
        Type = type;
        Value = value;
        Start = start;

        if (End is not null)
        {
            End = end;
        }
        else if (Start is not null)
        {
            End = Start.Copy();
            End.Advance(null);
        }
    }

    public override string ToString()
    {
        if (Value is not null)
        {
            return $"{Type}:{Value}";
        }

        return Type.ToString();
    }
}