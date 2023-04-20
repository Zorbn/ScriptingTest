public class Parser
{
    public struct ParserResult
    {
        public Node? Node;
        public Error? Error;
    }

    private List<Token> _tokens;
    private Token _currentToken;
    private int _tokenIndex = 1;

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
        _tokenIndex = -1;
        _currentToken = new Token(Token.TokenType.EOF);
        Advance();
    }

    private void Advance()
    {
        ++_tokenIndex;
        if (_tokenIndex < _tokens.Count)
        {
            _currentToken = _tokens[_tokenIndex];
        }
    }

    public ParserResult Parse()
    {
        var result = Expression();

        if (result.Error is null &&
            _currentToken.Type != Token.TokenType.EOF)
        {
            return new ParserResult
            {
                Error = new Error(Error.ErrorType.InvalidSyntax, "Incomplete expression", _currentToken.Start, _currentToken.End),
            };
        }

        return result;
    }

    private ParserResult Factor()
    {
        var token = _currentToken;

        if (token.Type == Token.TokenType.Plus ||
            token.Type == Token.TokenType.Minus)
        {
            Advance();
            var factor = Factor();
    
            if (factor.Error is not null) return factor;
    
            return new ParserResult
            {
                Node = new UnaryOpNode(token, factor.Node!),
                Error = null,
            };
        }
        else if (token.Type == Token.TokenType.Integer ||
                token.Type == Token.TokenType.Float)
        {
            Advance();
            return new ParserResult
            {
                Node = new NumberNode(token),
                Error = null,
            };
        }
        else if (token.Type == Token.TokenType.LeftParen)
        {
            Advance();
            var expression = Expression();
    
            if (expression.Error is not null) return expression;
            
            if (_currentToken.Type == Token.TokenType.RightParen)
            {
                Advance();
                return new ParserResult
                {
                    Node = expression.Node,
                    Error = null,
                };
            }
            else
            {
                return new ParserResult
                {
                    Node = null,
                    Error = new Error(Error.ErrorType.InvalidSyntax, "Expected ')'", _currentToken.Start, _currentToken.End),
                };
            }
        }

        var error = new Error(Error.ErrorType.InvalidSyntax, "Expected an int or a float", token.Start, token.End);

        return new ParserResult
        {
            Node = null,
            Error = error,
        };
    }

    private ParserResult BinaryOperation(Func<ParserResult> function, List<Token.TokenType> operators)
    {
        var left = function();

        if (left.Error is not null) return left;

        while (operators.Contains(_currentToken.Type))
        {
            var opToken = _currentToken;
            Advance();
            var right = function();

            if (right.Error is not null) return right;

            left = new ParserResult
            {
                Node = new BinOpNode(left.Node!, opToken, right.Node!),
                Error = null,
            };
        }

        return left;
    }

    private static readonly List<Token.TokenType> TermOperators = new()
    {
        Token.TokenType.Multiply,
        Token.TokenType.Divide,
    };

    private ParserResult Term()
    {
        return BinaryOperation(Factor, TermOperators);
    }

    private static readonly List<Token.TokenType> ExpressionOperators = new()
    {
        Token.TokenType.Plus,
        Token.TokenType.Minus,
    };

    private ParserResult Expression()
    {
        return BinaryOperation(Term, ExpressionOperators);
    }
}