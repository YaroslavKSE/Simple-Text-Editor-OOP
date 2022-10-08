using Simple_Text_Editor_OOP;

var savedText = new TextProcessor(new List<string[]>(), Array.Empty<string>());

while (true)
{
    Console.WriteLine("Write -help to see the commands");
    Console.WriteLine("Choose the command:");
    var userCommand = Console.ReadLine();
    if (userCommand == "0") break;
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
            var fileWriter = new FileWriter(fileName);
            fileWriter.WriteToFile(savedText.GetText());
            break;
        }
        case "5":
        {
            Console.WriteLine(savedText.GetText());
            break;
        }
    }

}