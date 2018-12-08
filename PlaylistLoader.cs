using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylistAnalyzer
{
    class PlaylistLoader
    {
        static int itemsInRow = 8;

        public static List<PlaylistStats> LoadList(string playlistFile)
        {
            List<PlaylistStats> musicPlaylist = new List<PlaylistStats>();

            try
            {
                using (StreamReader reader = new StreamReader(playlistFile))
                {
                    int currentLine = 0;
                    var headerRow = reader.ReadLine();
                    while(!(reader.EndOfStream))
                    {
                        currentLine++;

                        var line = reader.ReadLine();
                        var splits = line.Split('\t');

                        if(splits.Length != itemsInRow)
                        {
                            throw new Exception($"Row {currentLine} only has {splits.Length} different values. The line should contain {itemsInRow} different values.");
                        }

                        try
                        {
                            string name = splits[0].ToString();
                            string artist = splits[1].ToString();
                            string album = splits[2].ToString();
                            string genre = splits[3].ToString();
                            int size = Int32.Parse(splits[4]);
                            int time = Int32.Parse(splits[5]);
                            int year = Int32.Parse(splits[6]);
                            int plays = Int32.Parse(splits[7]);
                            PlaylistStats playlistStats = new PlaylistStats(name, artist, album, genre, size, time, year, plays);
                            musicPlaylist.Add(playlistStats);
                        }

                        catch (FormatException invalidItemNum)
                        {
                            throw new Exception($"Unknown data found in line {currentLine}. {invalidItemNum.Message}");
                        }
                    }
                }
            }

            catch (FileNotFoundException noFile)
            {
                throw new Exception($"The file {playlistFile} was not found. {noFile.Message}");
            }

            return musicPlaylist;
        }
    }
}
