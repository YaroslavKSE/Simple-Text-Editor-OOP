namespace Simple_Text_Editor_OOP;

public struct Cursor
{
    private int _line { get; set; }
    private int _index { get; set; }
    private string _text { get; set; }

    public Cursor(int line, int index, string text)
    {
        _line = line;
        _index = index;
        _text = text;
    }

    public int GetLine()
    {
        return _line;
    }
    public int GetIndex()
    {
        return _index;
    }

    public string GetText()
    {
        return _text;
    }
}