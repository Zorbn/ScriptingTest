public class Parser
{
    private Lexer _lexer;
    private Token _currentToken;
    
    public Parser(Lexer lexer)
    {
        _lexer = lexer;
        _currentToken = lexer.GetNextToken();
    }

    private void Eat(TokenType tokenType)
    {
        if (_currentToken.TokenType == tokenType)
        {
            _currentToken = _lexer.GetNextToken();
            return;
        }

        throw new Exception("Invalid syntax");
    }
     
    private Ast Factor()
    {
        var token = _currentToken;
        switch (token.TokenType)
        {
            case TokenType.Integer:
                Eat(TokenType.Integer);
                return new Number(token);
            case TokenType.LeftParen:
                Eat(TokenType.LeftParen);
                var result = Expression();
                Eat(TokenType.RightParen);
                return result;
        }

        throw new Exception("Failed to evaluate factor");
    }
    
    private Ast Term()
    {
        var node = Factor();
        
        while (_currentToken.TokenType is TokenType.Multiply or TokenType.Divide)
        {
            var token = _currentToken;

            switch (token.TokenType)
            {
                case TokenType.Multiply:
                    Eat(TokenType.Multiply);
                    break;                
                case TokenType.Divide:
                    Eat(TokenType.Divide);
                    break;
            }

            node = new BinaryOp(node, token, Factor());
        }

        return node;
    }
    
    private Ast Expression()
    {
        var node = Term();

        while (_currentToken.TokenType is TokenType.Plus or TokenType.Minus)
        {
            var token = _currentToken;

            switch (token.TokenType)
            {
                case TokenType.Plus:
                    Eat(TokenType.Plus);
                    break;
                case TokenType.Minus:
                    Eat(TokenType.Minus);
                    break;
            }

            node = new BinaryOp(node, token, Term());
        }

        return node;
    }
    
    public Ast Parse()
    {
        return Expression();
    }
}