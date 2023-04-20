public interface Node {}

public class NumberNode : Node
{
    public readonly Token Token;

    public NumberNode(Token token)
    {
        Token = token;
    }

    public override string ToString()
    {
        return $"{Token}";
    }
}

public class BinOpNode : Node
{
    public readonly Node LeftNode;
    public readonly Token OpToken;
    public readonly Node RightNode;

    public BinOpNode(Node leftNode, Token opToken, Node rightNode)
    {
        LeftNode = leftNode;
        OpToken = opToken;
        RightNode = rightNode;
    }

    public override string ToString()
    {
        return $"({LeftNode}, {OpToken}, {RightNode})";
    }
}

public class UnaryOpNode : Node
{
    public readonly Token OpToken;
    public readonly Node Node;

    public UnaryOpNode(Token opToken, Node node)
    {
        OpToken = opToken;
        Node = node;
    }

    public override string ToString()
    {
        return $"({OpToken}, {Node})";
    }
}
