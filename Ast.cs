public interface Ast
{

}

public struct BinaryOp : Ast
{
    public readonly Ast Left;
    public readonly Token Op;
    public readonly Ast Right;

    public BinaryOp(Ast left, Token op, Ast right)
    {
        Left = left;
        Op = op;
        Right = right;
    }
}

public struct UnaryOp : Ast
{
    public readonly Token Op;
    public readonly Ast Expression;

    public UnaryOp(Token op, Ast expression)
    {
        Op = op;
        Expression = expression;        
    }    
}

public struct Number : Ast
{
    public readonly Token Token;

    public Number(Token token)
    {
        Token = token;
    }    
}