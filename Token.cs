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
    }

    public readonly TokenType Type;
    public readonly dynamic? Value; // TODO: Figure out alternative to dynamic.

    public Token(TokenType type, dynamic? value = null)
    {
        Type = type;
        Value = value;
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