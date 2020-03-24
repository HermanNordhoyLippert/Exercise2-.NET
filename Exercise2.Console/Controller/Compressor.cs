using System;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using static System.Console;
using Exercise2.Console.View;

namespace Exercise2.Console.Controller
{
    /// <summary>
    /// Takes a choosen file then compress/decompressed it and times it. Then prints out the results
    /// </summary>
    public class Compressor
    {
        private static Stopwatch stopWatch = new Stopwatch();

        private string CreatePathToFile(string filename)
        {
            string directory = Directory.GetCurrentDirectory();
            return $"{directory}/Files/{filename}";
        }
        private string CreatePath()
        {
            string directory = Directory.GetCurrentDirectory();
            return $"{directory}/Files/";
        }
        private void IsTimerRunning(bool trigger)
        {
            if(trigger == true)
            {
                stopWatch.Start();
            }
            else
            {
                stopWatch.Stop();
                TimeSpan timeTaken = stopWatch.Elapsed;
                WriteLine("Execution time in seconds:" + timeTaken.TotalSeconds);
            }
        }
        public void Compress(string filename)
        {
            FileInfo fileToBeGZipped = new FileInfo(CreatePathToFile(filename));

            // Timing event
            IsTimerRunning(true);

            using (FileStream OriginalFile = fileToBeGZipped.OpenRead())
            {
                string compressedFileName = CreatePath() + "/compressed.gz";
                using (FileStream gzipCompressedFile = File.Create(compressedFileName))
                {
                    using (GZipStream gzipStream = new GZipStream(gzipCompressedFile, CompressionMode.Compress))
                    {
                        try
                        {
                            OriginalFile.CopyTo(gzipStream);
                            WriteLine("\nOriginal File: " + Path.GetFileName(OriginalFile.Name) + "[" + OriginalFile.Length + " bytes]");
                            WriteLine("New File: " + Path.GetFileName(gzipCompressedFile.Name) + "[" + gzipCompressedFile.Length + " bytes]");
                            IsTimerRunning(false);
                        }
                        catch (Exception ex)
                        {
                            WriteLine(ex.Message);
                        }
                    }
                }
            }
            //Return to menu
            MenuView.Menu();
        }
        public void Decompress(string filename)
        {
            FileInfo fileToBeGZipped = new FileInfo(CreatePathToFile(filename));
            FileInfo gzipFileName = new FileInfo(string.Concat(fileToBeGZipped.FullName));

            // Timing event
            IsTimerRunning(true);

            using (FileStream fileToDecompressAsStream = gzipFileName.OpenRead())
            {
                string decompressedFileName = CreatePath() + "/decompressed.txt";
                using (FileStream decompressedStream = File.Create(decompressedFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(fileToDecompressAsStream, CompressionMode.Decompress))
                    {
                        try
                        {
                            decompressionStream.CopyTo(decompressedStream);
                            // Creating report
                            WriteLine("\nOriginal File: " + Path.GetFileName(gzipFileName.Name) + "[" + gzipFileName.Length + " bytes]");
                            WriteLine("New File: " + Path.GetFileName(decompressedStream.Name) + "[" + decompressedStream.Length + " bytes]");
                            IsTimerRunning(false);
                        }
                        catch (Exception ex)
                        {
                            WriteLine(ex.Message);
                        }
                    }
                }
            }
            //Return to menu
            MenuView.Menu();
        }
    }
}
