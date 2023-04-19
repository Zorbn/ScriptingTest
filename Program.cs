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
            var interpreter = new Interpreter(lexer);
            var result = interpreter.Expression();

            Console.WriteLine(result);
        }
    }
}