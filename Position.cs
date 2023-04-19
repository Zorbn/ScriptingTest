public class Position
{
    public int Index;
    public int Line;
    public int Column;
    public string FileName = "";
    public string FileText = "";

    public void Advance(char? currentChar)
    {
        ++Index;
        ++Column;

        if (currentChar == '\n')
        {
            ++Line;
            Column = 0;
        }
    }
    
    public Position Copy()
    {
        return new Position
        {
            Index = Index,
            Line = Line,
            Column = Column,
            FileName = FileName,
            FileText = FileText,
        };
    }
}