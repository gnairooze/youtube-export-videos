// See https://aka.ms/new-console-template for more information
using youtube.export.videos.app;

Console.Title = "Export Youtube Videos";

while (true)
{
    var answer = AskQuestion();

    Console.WriteLine();

    switch (answer.Key)
    {
        case ConsoleKey.NumPad1:
        case ConsoleKey.D1:
            await ExportVideos.ExportChannel();
            break;
        case ConsoleKey.NumPad2:
        case ConsoleKey.D2:
            await ExportVideos.ExportUser();
            break;
        case ConsoleKey.NumPad3:
        case ConsoleKey.D3:
            await ExportVideos.ExportSlug();
            break;
        case ConsoleKey.NumPad4:
        case ConsoleKey.D4:
            Environment.Exit(0);
            break;
    }
}

static ConsoleKeyInfo AskQuestion()
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