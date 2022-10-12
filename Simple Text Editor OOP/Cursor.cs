namespace Simple_Text_Editor_OOP;

public  class Cursor
{
    private int Line { get; }
    private int Index { get; }
    private string Text { get; }

    public Cursor(int line, int index, string text)
    {
        Line = line;
        Index = index;
        Text = text;
    }

    public int GetLine()
    {
        return Line;
    }

    public int GetIndex()
    {
        return Index;
    }

    public string GetText()
    {
        return Text;
    }
}