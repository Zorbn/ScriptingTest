using System.Text;

public class Lexer
{
    public struct LexerResult
    {
        public List<Token> Tokens;
        public Error? Error;
    }

    private const string Digits = "0123456789";

    private string _fileName;
    private string _fileText;
    private Position _position;

    private char? _currentChar = null;

    public Lexer(string fileName, string fileText)
    {
        _fileName = fileName;
        _fileText = fileText;
        
        _position = new Position
        {
            Index = -1,
            Line = 0,
            Column = -1,
            FileName = fileName,
            FileText = fileText,
        };

        Advance();
    }
    
    public void Advance()
    {
        _position.Advance(_currentChar);

        if (_position.Index < _fileText.Length)
        {
            _currentChar = _fileText[_position.Index];
        }
        else {
            _currentChar = null;
        }
    }
    
    public LexerResult MakeTokens()
    {
        var tokens = new List<Token>();

        while (_currentChar != null)
        {
            char currentChar = _currentChar.Value;
            if (Digits.Contains(currentChar))
            {
                tokens.Add(MakeNumber());
                continue;
            }

            // TODO: Can Advance be split out of this?
            switch (currentChar)
            {
                case ' ':
                case '\t':
                    Advance();                
                    break;
                case '+':
                    tokens.Add(new Token(Token.TokenType.Plus));
                    Advance();
                    break;
                case '-':
                    tokens.Add(new Token(Token.TokenType.Minus));
                    Advance();
                    break;
                case '*':
                    tokens.Add(new Token(Token.TokenType.Multiply));
                    Advance();
                    break;
                case '/':
                    tokens.Add(new Token(Token.TokenType.Divide));
                    Advance();
                    break;
                case '(':
                    tokens.Add(new Token(Token.TokenType.LeftParen));
                    Advance();
                    break;
                case ')':
                    tokens.Add(new Token(Token.TokenType.RightParen));
                    Advance();
                    break;
                default:
                    var illegalChar = currentChar;
                    var start = _position.Copy();
                    tokens.Clear();
                    Advance();
                    var end = _position;
                    return new LexerResult
                    {
                        Tokens = tokens,
                        Error = new Error(Error.ErrorType.IllegalCharacter, start, end, $"'{illegalChar}'"),
                    };
            }

        }

        return new LexerResult
        {
            Tokens = tokens,
            Error = null,
        };
    }
    
    private Token MakeNumber()
    {
        var numberString = new StringBuilder();
        int dotCount = 0;
        
        while (_currentChar is not null && (Digits.Contains(_currentChar.Value) || _currentChar == '.')) 
        {
            if (_currentChar == '.')
            {
                if (dotCount > 0)
                {
                    // Numbers can't have more than one dot.
                    break;
                }

                ++dotCount;
                numberString.Append('.');
            }
            else
            {
                numberString.Append(_currentChar);
            }

            Advance();
        }
        
        if (dotCount == 0)
        {
            // The number is an integer.
            return new Token(Token.TokenType.Integer, Int32.Parse(numberString.ToString()));
        }

        // The number is a float.
        return new Token(Token.TokenType.Integer, Single.Parse(numberString.ToString()));
    }
}