internal class Program
{
    private static void Main()
    {
        while (true)
        {
            Console.Write("CALC > ");
            var text = Console.ReadLine();
            
            if (text is null) continue;

            var lexer = new Lexer(text);
            var parser = new Parser(lexer);
            var interpreter = new Interpreter(parser);
            var result = interpreter.Interpret();

            Console.WriteLine(result);
        }
    }
}