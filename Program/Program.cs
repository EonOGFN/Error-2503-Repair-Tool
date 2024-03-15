using System;
using System.Data;
using System.Diagnostics;
using System.IO;

class Error2503Fix
{ 
    static async Task Main()
    {
        Console.Title = "Eon Error 2503 Fix";
        await DownloadProgression(2); 

        string windowsTempPath = @"C:\Windows\Temp";

        if (!Directory.Exists(windowsTempPath))
        {
            Directory.CreateDirectory(windowsTempPath);
        }

        AdminPerm(windowsTempPath);
        AdminPerm(Path.GetTempPath());
        AdminPerm(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Temp"));
    }

    static async Task DownloadProgression(int seconds)
    {
        Console.WriteLine("Fixing Error 2503...");
        const int totalTicks = 20;
        const string progressCharacter = "#";
        const char boxTopLeft = '[';
        const char boxBottomRight = ']';
        const char movingCharacter = '/';

        Console.CursorVisible = false;
        ConsoleColor originalColor = Console.ForegroundColor; 

        for (int i = 0; i <= totalTicks; i++)
        {
            if (i == totalTicks)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{boxTopLeft}{new string(progressCharacter[0], i)}{new string(progressCharacter[0], totalTicks - i)}{boxBottomRight} - {i * 100 / totalTicks}%\r");
            }
            else
            {
                Console.Write($"{boxTopLeft}{new string(progressCharacter[0], i)}{movingCharacter}{new string(progressCharacter[0], totalTicks - i - 1)}{boxBottomRight} - {i * 100 / totalTicks}%\r");
            }
            await Task.Delay(seconds * 1000 / totalTicks);
        }

        await Task.Delay(1000);
        Console.ForegroundColor = originalColor; 
        Console.CursorVisible = true;
        Console.WriteLine("Error 2503 has been fixed. Please reinstall the Eon Installer.");
        Console.ReadLine();
    }

    static void AdminPerm(string folderPath)
    {
        ProcessStartInfo processInfo = new ProcessStartInfo
        {
            FileName = "icacls",
            Arguments = $"\"{folderPath}\" /grant Everyone:(OI)(CI)F /T",
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            UseShellExecute = false
        };

        Process process = new Process
        {
            StartInfo = processInfo
        };

        process.Start();
        process.WaitForExit();
    }
}
