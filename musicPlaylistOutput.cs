using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylistAnalyzer
{
    class musicPlaylistOutput
    {
        public static string ReportCreator(List<PlaylistStats> musicPlaylist)
        {
            string outputFile = "New Music Playlist\n\n";

            if (musicPlaylist.Count < 1)
            {
                outputFile += "No data found...\n";
                return outputFile;
            }

            outputFile += "How many songs received 200 or more plays?\n";
            var numPlays = from PlaylistStats in musicPlaylist where PlaylistStats.Plays >= 200 select PlaylistStats.Plays;
            outputFile += $"There are {numPlays.Count()} songs with over 200 plays within this playlist.\n\n";

            outputFile += "How many songs are in the playlist with the genre \"Alternative\"?\n";
            var altGenre = from PlaylistStats in musicPlaylist where PlaylistStats.Genre == "Alternative" select PlaylistStats.Genre;
            outputFile += $"There are {altGenre.Count()} \"Alternative\" genre songs in this playlist.\n\n";

            outputFile += "How many songs are in the playlist with the Genre of \"Hip-Hop/Rap\"?\n";
            var rapGenre = from PlaylistStats in musicPlaylist where PlaylistStats.Genre == "Hip-Hop/Rap" select PlaylistStats.Genre;
            outputFile += $"There are {rapGenre.Count()} \"Hip-Hop/Rap\" genre songs in this playlist.\n\n";

            outputFile += "What songs are in the playlist from the album \"Welcome to the Fishbowl?\"\n";
            var specificAlbum = from PlaylistStats in musicPlaylist where PlaylistStats.Album == "Welcome to the Fishbowl" select PlaylistStats.Name;
            foreach (var song in specificAlbum)
                outputFile += $"{song}\n";
            outputFile += "\n";

            outputFile += "What are the songs in the playlist from before 1970?\n";
            var specificYear = from PlaylistStats in musicPlaylist where PlaylistStats.Year < 1970 select PlaylistStats.Name;
            foreach (var oldies in specificYear)
                outputFile += $"{oldies}\n";
            outputFile += "\n";

            outputFile += "What are the song names that are more than 85 characters long?\n";
            var longNames = from PlaylistStats in musicPlaylist where PlaylistStats.Name.Length > 85 select PlaylistStats.Name;
            foreach (var lengthyNames in longNames)
                outputFile += $"{lengthyNames}\n";
            outputFile += "\n";

            int longestSong = musicPlaylist.Max(PlaylistStats => PlaylistStats.Time);
            outputFile += "What is the longest song? (longest in Time)\n";
            var duration = from PlaylistStats in musicPlaylist where PlaylistStats.Time == longestSong select PlaylistStats.Name;
            foreach (var longestTimeName in duration)
                outputFile += $"{longestTimeName} is the longest song, with a duration of {longestSong} seconds.\n";
            outputFile += "\n";

            return outputFile;
        }
    }
}
