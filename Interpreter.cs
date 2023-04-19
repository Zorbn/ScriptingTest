public class Interpreter
{
    // TODO: Can this be static?
    public Dictionary<Type, Func<Ast, int>> VisitorFunctions;

    private Parser _parser;

    public Interpreter(Parser parser)
    {
        _parser = parser;

        VisitorFunctions = new()
        {
            { typeof(BinaryOp), VisitBinaryOp },
            { typeof(Number), VisitNumber },
        };        
    }
    
    public int Interpret()
    {
        var ast = _parser.Parse();
        return Visit(ast);
    }

    public int Visit(Ast node)
    {
        return VisitorFunctions[node.GetType()](node);
    }
    
    private int VisitBinaryOp(Ast node)
    {
        var binaryOp = (BinaryOp)node;

        return binaryOp.Op.TokenType switch
        {
            TokenType.Plus => Visit(binaryOp.Left) + Visit(binaryOp.Right),
            TokenType.Minus => Visit(binaryOp.Left) - Visit(binaryOp.Right),
            TokenType.Multiply => Visit(binaryOp.Left) * Visit(binaryOp.Right),
            TokenType.Divide => Visit(binaryOp.Left) / Visit(binaryOp.Right),
            _ => throw new Exception($"Failed to visit invalid type {binaryOp.Op.TokenType}"),
        };
    }

    private int VisitNumber(Ast node)
    {
        var number = (Number)node;
        return number.Token.Value!;
    }
}