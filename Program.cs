using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylistAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("MusicPlaylistAnalyzer <Music_Playlist_File_Path> <Output_File_Path>");
                Environment.Exit(1);
            }

            string playlistFile = args[0];
            string reportFile = args[1];

            if (!File.Exists(reportFile))
                using (StreamWriter newReport = new StreamWriter(reportFile))
                    File.Create(reportFile);
            else
                using (StreamWriter newReport = new StreamWriter(reportFile))
                    newReport.WriteLine(string.Empty);

            List<PlaylistStats> musicPlaylist = null;

            try
            {
               musicPlaylist = PlaylistLoader.LoadList(playlistFile);
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.Message);
                return;
            }

            var output = musicPlaylistOutput.ReportCreator(musicPlaylist);

            try
            {
                using (StreamWriter outputFile = new StreamWriter(reportFile))
                {
                        outputFile.WriteLine(output);
                }
            }

            catch (Exception noOutput)
            {
                Console.WriteLine(noOutput.Message);
                Environment.Exit(3);
            }

            Console.WriteLine("Report complete... Press any key to continue.\n");
            Console.ReadKey();
        }
    }
}
