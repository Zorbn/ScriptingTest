public enum TokenType
{
    Integer,
    Plus,
    Minus,
    Multiply,
    Divide,
    LeftParen,
    RightParen,
    Eof,
}

public struct Token
{
    public readonly TokenType TokenType;
    public readonly dynamic? Value; // TODO: Stop using dynamic?


    public Token(TokenType type, dynamic? value)
    {
        TokenType = type;
        Value = value;
    }

    public override string ToString()
    {
        return $"Token({TokenType}, {Value})";
    }
}