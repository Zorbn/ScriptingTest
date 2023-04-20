public struct Number
{
    public readonly dynamic Value;
    public readonly Position Start;
    public readonly Position End;
    public readonly Context Context;

    public Number(dynamic value, Position start, Position end, Context context)
    {
        Value = value;
        Start = start;
        End = end;
        Context = context;
    }
    
    public RuntimeResult Add(Number other)
    {
        return new RuntimeResult
        {
            Number = new Number(Value + other.Value, Start, other.End, Context),
        };
    }

    public RuntimeResult Subtract(Number other)
    {
        return new RuntimeResult
        {
            Number = new Number(Value - other.Value, Start, other.End, Context),
        };
    }

    public RuntimeResult Multiply(Number other)
    {
        return new RuntimeResult
        {
            Number = new Number(Value * other.Value, Start, other.End, Context),
        };
    }

    public RuntimeResult Divide(Number other)
    {
        if (other.Value == 0)
        {
            return new RuntimeResult
            {
                Error = new Error(Error.ErrorType.RuntimeError, "Can't divide by zero", Start, other.End, Context),
            };
        }

        return new RuntimeResult
        {
            Number = new Number(Value / other.Value, Start, other.End, Context),
        };
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}