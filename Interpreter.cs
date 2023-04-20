public class Interpreter
{
    private Dictionary<Type, Func<Node, Context, RuntimeResult>> VisitorFunctions;

    public Interpreter()
    {
        VisitorFunctions = new()
        {
            { typeof(NumberNode), VisitNumberNode },
            { typeof(BinOpNode), VisitBinOpNode },
            { typeof(UnaryOpNode), VisitUnaryOpNode },
        };
    }

    public RuntimeResult Visit(Node node, Context context)
    {
        return VisitorFunctions[node.GetType()](node, context);
    }

    private RuntimeResult VisitNumberNode(Node node, Context context)
    {
        var number = (NumberNode)node;
        return new RuntimeResult
        {
            Number = new Number(number.Token.Value!, number.Token.Start, number.Token.End, context),
        };
    }

    private RuntimeResult VisitBinOpNode(Node node, Context context)
    {
        var binOp = (BinOpNode)node;

        var left = Visit(binOp.LeftNode, context);
        if (left.Error is not null) return left;
        var leftNumber = left.Number!.Value;

        var right = Visit(binOp.RightNode, context);
        if (right.Error is not null) return right;
        var rightNumber = right.Number!.Value;

        var result = binOp.OpToken.Type switch
        {
            Token.TokenType.Plus => leftNumber.Add(rightNumber),
            Token.TokenType.Minus => leftNumber.Subtract(rightNumber),
            Token.TokenType.Multiply => leftNumber.Multiply(rightNumber),
            Token.TokenType.Divide => leftNumber.Divide(rightNumber),
            _ => throw new Exception("Failed to evaluate binary operator"),
        };

        return result;
    }

    private RuntimeResult VisitUnaryOpNode(Node node, Context context)
    {
        var unaryOp = (UnaryOpNode)node;
        var number = Visit(unaryOp.Node, context).Number!.Value;

        var resultNumber = unaryOp.OpToken.Type switch
        {
            Token.TokenType.Plus => number,
            Token.TokenType.Minus => new Number(-number.Value, number.Start, number.End, context),
            _ => throw new Exception("Failed to evaluate unary operator"),
        };

        return new RuntimeResult
        {
            Number = resultNumber,
        };
    }
}