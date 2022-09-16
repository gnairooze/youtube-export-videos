// See https://aka.ms/new-console-template for more information
using youtube.export.videos.app;
using youtube.export.videos.app.output;

Console.Title = "Export Youtube Videos";

Markdown _Markdown = new();
HTML _HTML = new();

while (true)
{
    var operation = AskOperation();
    Console.WriteLine();

    var output = AskOutput();
    Console.WriteLine();

    IOutput outputHandler = CreateOutputHandler(output);

    switch (operation.Key)
    {
        case ConsoleKey.NumPad1:
        case ConsoleKey.D1:
            await ExportVideos.ExportChannel(outputHandler);
            break;
        case ConsoleKey.NumPad2:
        case ConsoleKey.D2:
            await ExportVideos.ExportUser(outputHandler);
            break;
        case ConsoleKey.NumPad3:
        case ConsoleKey.D3:
            await ExportVideos.ExportSlug(outputHandler);
            break;
        case ConsoleKey.NumPad4:
        case ConsoleKey.D4:
            Environment.Exit(0);
            break;
    }
}

IOutput CreateOutputHandler(ConsoleKeyInfo output)
{
    while (true)
    {
        switch (output.Key)
        {
            case ConsoleKey.NumPad1:
            case ConsoleKey.D1:
                return _Markdown;
            case ConsoleKey.NumPad2:
            case ConsoleKey.D2:
                return _HTML;
        }

        output = AskOutput();
    }
}

static ConsoleKeyInfo AskOutput()
{
    Console.WriteLine($"{Environment.NewLine}");
    Console.WriteLine("What is the type of output format:");
    Console.WriteLine("1. Markdown");
    Console.WriteLine("2. HTML");
    Console.WriteLine();

    var answer = Console.ReadKey();
    return answer;
}

static ConsoleKeyInfo AskOperation()
{
    Console.WriteLine($"{Environment.NewLine}");
    Console.WriteLine("Please select one of the following:");
    Console.WriteLine("1. Export videos by Youtube channel url (example: https://www.youtube.com/channel/[code])");
    Console.WriteLine("2. Export videos by Youtube user url (example: https://www.youtube.com/user/[username])");
    Console.WriteLine("3. Export videos by Youtube slug url (example: https://www.youtube.com/c/[name])");
    Console.WriteLine("4. Exit");
    Console.WriteLine();

    var answer = Console.ReadKey();
    return answer;
}