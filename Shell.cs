using System;

public class Shell
{
    public void Start()
    {
        while (true)
        {
            Console.Write("BASIC > ");
            var input = Console.ReadLine();
            
            if (input is null) continue;

            var result = Run("REPL", input);
            
            if (result.Error is not null)
            {
                Console.WriteLine(result.Error);
            }
            else
            {
                // foreach (var token in result.Tokens)
                // {
                //     Console.Write($"{token} ");
                // }
                Console.Write($"{result.Number}");

                Console.Write("\n");
            }
        }
    }

    public RuntimeResult Run(string fileName, string fileText)
    {
        var lexer = new Lexer(fileName, fileText);
        var lexerResult = lexer.MakeTokens();

        if (lexerResult.Error is not null)
        {
            return new RuntimeResult
            {
                Error = lexerResult.Error,
            };
        }

        var parser = new Parser(lexerResult.Tokens);
        var ast = parser.Parse();

        if (ast.Error is not null)
        {
            return new RuntimeResult
            {
                Error = ast.Error,
            };
        }

        var interpreter = new Interpreter();
        var context = new Context("<program>");
        var result = interpreter.Visit(ast.Node!, context);

        return result;
    }
}