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
                Console.Write($"{result.Node}");

                Console.Write("\n");
            }
        }
    }

    public Parser.ParserResult Run(string fileName, string fileText)
    {
        var lexer = new Lexer(fileName, fileText);
        var result = lexer.MakeTokens();
        
        if (result.Error is not null)
        {
            return new Parser.ParserResult
            {
                Node = null,
                Error = result.Error,
            };
        }

        var parser = new Parser(result.Tokens);
        var ast = parser.Parse();

        return ast;
    }
}