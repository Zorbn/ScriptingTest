public interface Node {}

public class NumberNode : Node
{
    private Token _token;

    public NumberNode(Token token)
    {
        _token = token;
    }

    public override string ToString()
    {
        return $"{_token}";
    }
}

public class BinOpNode : Node
{
    private Node _leftNode;
    private Token _opToken;
    private Node _rightNode;

    public BinOpNode(Node leftNode, Token opToken, Node rightNode)
    {
        _leftNode = leftNode;
        _opToken = opToken;
        _rightNode = rightNode;
    }

    public override string ToString()
    {
        return $"({_leftNode}, {_opToken}, {_rightNode})";
    }
}

public class UnaryOpNode : Node
{
    private Token _opToken;
    private Node _node;

    public UnaryOpNode(Token opToken, Node node)
    {
        _opToken = opToken;
        _node = node;
    }

    public override string ToString()
    {
        return $"({_opToken}, {_node})";
    }
}
