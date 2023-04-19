public class Interpreter
{
    private Lexer _lexer;
    private Token _currentToken;
    
    public Interpreter(Lexer lexer)
    {
        _lexer = lexer;
        _currentToken = lexer.GetNextToken();
    }

    public void Eat(TokenType tokenType)
    {
        if (_currentToken.TokenType == tokenType)
        {
            _currentToken = _lexer.GetNextToken();
            return;
        }

        throw new Exception("Invalid syntax");
    }
     
    private dynamic Factor()
    {
        var token = _currentToken;
        Eat(TokenType.Integer);
        return token.Value!;
    }
    
    // Expression -> Integer Plus Integer
    // Expression -> Integer Minus Integer
    public int Expression()
    {
        var result = Factor();

        while (_currentToken.TokenType is TokenType.Multiply or TokenType.Divide)
        {
            switch (_currentToken.TokenType)
            {
                case TokenType.Multiply:
                    Eat(TokenType.Multiply);
                    result *= Factor();
                    break;
                case TokenType.Divide:
                    Eat(TokenType.Divide);
                    result /= Factor();
                    break;
            }
        }

        return result;
    }
}