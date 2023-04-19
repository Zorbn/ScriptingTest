using System.Text;

public class Lexer
{
    private string _text;
    private int _position = 0;
    private char? _currentChar;

    public Lexer(string text)
    {
        _text = text;
        _currentChar = _text[_position];
    }

    private void Advance()
    {
        ++_position;

        if (_position >= _text.Length)
        {
            _currentChar = null;
            return;
        }

        _currentChar = _text[_position];
    }
    
    private int GetInteger()
    {
        var result = new StringBuilder();
        while (_currentChar is not null && Char.IsDigit(_currentChar.Value))
        {
            result.Append(_currentChar);
            Advance();
        }

        return Int32.Parse(result.ToString());
    }
    
    private void GetWhitespace()
    {
        while (_currentChar is not null && Char.IsWhiteSpace(_currentChar.Value))
        {
            Advance();
        }
    }
 
    public Token GetNextToken()
    {
        GetWhitespace();

        while (_currentChar is not null)
        {
            if (Char.IsDigit(_currentChar.Value))
            {
                return new Token(TokenType.Integer, GetInteger());
            }

            switch (_currentChar)
            {
                case '+':
                    Advance();
                    return new Token(TokenType.Plus, '+');
                case '-':
                    Advance();
                    return new Token(TokenType.Minus, '+');
                case '*':
                    Advance();
                    return new Token(TokenType.Multiply, '*');
                case '/':
                    Advance();
                    return new Token(TokenType.Divide, '/');
                case '(':
                    Advance();
                    return new Token(TokenType.LeftParen, '(');
                case ')':
                    Advance();
                    return new Token(TokenType.RightParen, ')');
            }

            throw new Exception("Error getting next token");
        }

        return new Token(TokenType.Eof, null);
    }
}