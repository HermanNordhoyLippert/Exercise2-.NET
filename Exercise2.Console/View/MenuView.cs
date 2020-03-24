using System.IO;
using System.Linq;
using static System.Console;
using Exercise2.Console.Controller;

namespace Exercise2.Console.View
{
    /// <summary>
    /// Prints out a menu for the user to navigtaion trough the application
    /// </summary>
    public class MenuView
    {
        private static string userInput;
        private static string path;
        private static Compressor compressor = new Compressor();
        public MenuView()
        {
            string directory = Directory.GetCurrentDirectory();
            path = directory + "/Files";

            WriteLine("Exercise2 by Herman Nordhøy Lippert");
            Menu();
        }
        public static void Menu()
        {
            WriteLine("\n" +
                "Please a service" + "\n" +
                "1. Compress" + "\n" +
                "2. Decompress" + "\n" +
                "3. Open Folder" + "\n" +
                "4. Exit" + "\n");

            //Get user's input
            userInput = ReadLine();

            switch (userInput)
            {
                case "1":
                    FilePicker("Compress");
                    break;

                case "2":
                    FilePicker("Decompress");
                    break;
                case "3":
                    OpenFolder();
                    Menu();
                    break;
                case "4":
                    System.Environment.Exit(1);
                    break;

                default:
                    WriteLine("Wrong user input");
                    Menu();
                    break;
            }
        }
        private static void FilePicker(string choice)
        {
            WriteLine("What file do you want to " + choice + "?");
            foreach (string s in Directory.GetFiles(path, "").Select(Path.GetFileName))
            {
                WriteLine(s);
            }
            WriteLine("\nPlease type in full name of file:");
            string chosenFile = ReadLine();

            CompressOrDecompress(choice, chosenFile);
        }
        private static void CompressOrDecompress(string choice, string chosenFile)
        {
            switch (choice)
            {
                case "Compress":
                    compressor.Compress(chosenFile);
                    break;

                case "Decompress":
                    //compressor.Decompress(chosenFile);
                    break;

                default:
                    WriteLine("Something wrong happend");
                    break;
            }
            ReadLine();
        }
        private static void OpenFolder()
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true,
                Verb = "open"
            });
        }
    }
}
