using Simple_Text_Editor_OOP;

var savedText = new TextProcessor(new List<char[]>(), Array.Empty<char>());

var console = new ConsoleParser("", Array.Empty<int>());

string[] commands =
{
    "0.  End program",
    "1.  Append text symbols to the end",
    "2.  Start the new line",
    "3.  Saving the information",
    "4.  Load the information",
    "5.  Print the current text to console",
    "6.  Insert the text by line and symbol index",
    "7.  Search",
    "8.  Clearing the console", // not implemented
    "8.  Delete command",
    "9.  Undo command",
    "10. Redo command",
    "11. Cut command",
    "12. Paste command",
    "13. Copy command",
    "14. Insert with replacement command"
};

while (true)
{
    Console.WriteLine("Write -help to see the commands");
    Console.WriteLine("Choose the command:");
    var userCommand = Console.ReadLine();
    if (userCommand == "0") break;
    if (userCommand == "-help")
        foreach (var command in commands)
        {
            Console.WriteLine(command);
        }

    switch (userCommand)
    {
        case "1":
        {
            console.AskForText(1);
            savedText.AddText(console.GetInput());
            break;
        }
        case "2":
        {
            console.AskForText(2);
            savedText.AddNewLine();
            savedText.AddText(console.GetInput());
            break;
        }
        case "3":
        {
            console.AskForText(3);
            var path = $@"D:\C#\Simple Text Editor OOP\Simple Text Editor OOP\{console.GetInput()}";
            var fileWriter = new FileWriter(path);
            fileWriter.WriteToFile(savedText.GetText());
            break;
        }
        case "4":
        {
            console.AskForText(4);
            var path = $@"D:\C#\Simple Text Editor OOP\Simple Text Editor OOP\{console.GetInput()}";
            if (console.GetInput().EndsWith(".txt") && File.Exists(path))
            {
                var fileReader = new FileReader(path);
                var lines = fileReader.ReadFile();
                savedText.ClearText();
                savedText.LoadToMemory(lines);
            }

            break;
        }
        case "5":
        {
            Console.WriteLine(savedText.GetText());
            break;
        }
        case "6":
        {
            console.AskForTextLineAndIndex();
            savedText.AddTextInside(console.GetInput(), console.GetLine(), console.GetIndex());
            break;
        }
        case "7":
        {
            console.AskForText(7);
            Console.WriteLine($"Founded '{console.GetInput()}' {savedText.SearchSubstring(console.GetInput())}");
            break;
        }
        case "8":
        {
            console.AskForLineIndexAndLength();
            savedText.Delete(console.GetLine(), console.GetIndex(), console.GetLength());
            break;
        }
        case "9":
        {
            savedText.Undo();
            break;
        }
        case "10":
        {
            savedText.Redo();
            break;
        }
        case "11":
            console.AskForLineIndexAndLength();
            savedText.CutArray(console.GetLine(), console.GetIndex(), console.GetLength());
            break;
        case "12":
        {
            console.AskForLineAndIndex();
            savedText.Paste(console.GetLine(), console.GetIndex());
            break;
        }
        case "13":
        {
            console.AskForLineIndexAndLength();
            savedText.Copy(console.GetLine(), console.GetIndex(), console.GetLength());
            break;
        }
        case "14":
        {
            console.AskForTextLineAndIndex();
            savedText.Insert(console.GetLine(), console.GetIndex(), console.GetInput());
            break;
        }
    }
}