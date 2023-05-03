using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooreMediaPlayer
{
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public TimeSpan Duration { get; set; }
        public string FilePath { get; set; }

        /// <summary>
        /// Constructor for song object
        /// </summary>
        /// <param name="title"></param>
        /// <param name="artist"></param>
        /// <param name="album"></param>
        /// <param name="duration">TimeSpan object for tracking duration of song with timer object</param>
        /// <param name="filePath"></param>
        public Song(string title, string artist, string album, TimeSpan duration, string filePath)
        {
            Title = title;
            Artist = artist;
            Album = album;
            Duration = duration;
            FilePath = filePath;
        }

        public Song()
    {
        Title = "";
        Artist = "";
        Album = "";
        Duration = TimeSpan.Zero;
        FilePath = "";
    }
    }

}
