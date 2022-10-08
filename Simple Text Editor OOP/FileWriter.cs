namespace Simple_Text_Editor_OOP;

public class FileWriter
{
    private readonly string _path;

    public FileWriter(string path)
    {
        _path = path;
    }

    public void WriteToFile(string text)
    {
        if (_path.EndsWith(".txt"))
        {
            using StreamWriter file = new(_path);
            file.WriteLineAsync(text);
        }
    }
}