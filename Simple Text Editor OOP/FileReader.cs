namespace Simple_Text_Editor_OOP;

public class FileReader
{
    private string _path;

    public FileReader(string path)
    {
        _path = path;
    }

    public string[] ReadFile()
    {
        var lines = File.ReadAllLines(_path);
        return lines;
    }
}


