namespace Simple_Text_Editor_OOP;

public class ConsoleParser
{
    private string _userInput;
    private int[] _lineIndexLength;


    public ConsoleParser(string userInput, int[] lineIndexLength)
    {
        _userInput = userInput;
        _lineIndexLength = lineIndexLength;
    }

    public string GetInput()
    {
        return _userInput;
    }

    public int GetLine()
    {
        return _lineIndexLength.Length > 0 ? _lineIndexLength[0] : 0;
    }

    public int GetIndex()
    {
        return _lineIndexLength.Length > 1 ? _lineIndexLength[1] : 0;
    }

    public int GetLength()
    {
        return _lineIndexLength.Length > 2 ? _lineIndexLength[2] : 0;
    }


    public void AskForText(int command)
    {
        switch (command)
        {
            case 1:
                Console.WriteLine("Enter text to append:");
                break;
            case 2:
                Console.WriteLine("New line started. Enter text to append:");
                break;
            case 3:
                Console.WriteLine("Enter the file name for saving:");
                break;
            case 4:
                Console.WriteLine("Enter the file name for loading:");
                break;
            case 7:
                Console.WriteLine("Choose world to search");
                break;
        }

        _userInput = Console.ReadLine()!;
    }

    public void AskForLineAndIndex()
    {
        Console.WriteLine("Choose line, and index: ");
        var userInput = Console.ReadLine()!.Split(' ');
        _lineIndexLength = new int[userInput.Length];
        for (int i = 0; i < userInput.Length; i++)
        {
            _lineIndexLength[i] = int.Parse(userInput[i]);
        }
    }

    public void AskForTextLineAndIndex()
    {
        Console.WriteLine("Choose line and index:");
        var userInput = Console.ReadLine()!.Split(' ');
        _lineIndexLength = new int[userInput.Length];
        for (int i = 0; i < userInput.Length; i++)
        {
            _lineIndexLength[i] = int.Parse(userInput[i]);
        }

        Console.WriteLine("Enter text to insert:");
        _userInput = Console.ReadLine()!;
    }

    public void AskForLineIndexAndLength()
    {
        Console.WriteLine("Choose line, index and number of symbols: ");
        var userInput = Console.ReadLine()!.Split(' ');
        _lineIndexLength = new int[userInput.Length];
        for (int i = 0; i < userInput.Length; i++)
        {
            _lineIndexLength[i] = int.Parse(userInput[i]);
        }
    }
}