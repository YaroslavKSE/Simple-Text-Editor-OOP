using Simple_Text_Editor_OOP;

var savedText = new TextProcessor(new List<string[]>(), Array.Empty<string>());

string[] commands =
{
    "0. End program",
    "1. Append text symbols to the end",
    "2. Start the new line",
    "3. Saving the information",
    "4. Load the information",
    "5. Print the current text to console",
    "6. Insert the text by line and symbol index",
    "7. Search",
    "8. Clearing the console"
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
            Console.WriteLine("Enter text to append:");
            var userInput = Console.ReadLine();
            savedText.AddText(userInput);
            break;
        }
        case "2":
        {
            Console.WriteLine("New line started. Enter text to append:");
            var userInput2 = Console.ReadLine();
            savedText.AddNewLine();
            savedText.AddText(userInput2);
            break;
        }
        case "3":
        {
            Console.WriteLine("Enter the file name for saving:");
            var fileName = Console.ReadLine();
            var path = $@"D:\C#\Simple Text Editor OOP\Simple Text Editor OOP\{fileName}";
            var fileWriter = new FileWriter(path);
            fileWriter.WriteToFile(savedText.GetText());
            break;
        }
        case "4":
        {
            Console.WriteLine("Enter the file name for loading:");
            string? fileNameRead = Console.ReadLine();
            var path = $@"D:\C#\Simple Text Editor OOP\Simple Text Editor OOP\{fileNameRead}";
            if (fileNameRead!.EndsWith(".txt") && File.Exists(path))
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
            Console.WriteLine("Choose line and index:");
            var userInput3 = Console.ReadLine()!.Split(' ');
            Console.WriteLine("Enter text to insert:");
            var userInput4 = Console.ReadLine();
            savedText.AddTextInside(userInput4, int.Parse(userInput3[0]), int.Parse(userInput3[1]));
            break;
        }
        case "7":
        {
            Console.WriteLine("Choose world to search");
            var userInput5 = Console.ReadLine();
            Console.WriteLine($"Founded '{userInput5}' {savedText.SearchSubstring(userInput5)}");
            break;
        }
        case "8":
        {
            Console.WriteLine("Choose line, index and number of symbols: ");
            var userInput6 = Console.ReadLine()!.Split(' ');
            var line = int.Parse(userInput6[0]);
            var index = int.Parse(userInput6[1]);
            var length = int.Parse(userInput6[2]);
            savedText.Delete(line, index, length);
            break;
        }
    }
}