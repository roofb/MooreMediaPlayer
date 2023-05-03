using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TagLib;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Policy;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace MooreMediaPlayer
{
    /// <summary>
    /// Window that opens when the "New..." button is pressed, opens dialog window with options "Current Library, Album, Playlist
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Click for New... --> Current library, opens file dialog and lets user choose folderpath
        /// Metadata from songs in folder is loaded into song objects and into an ObservableCollection which is assigned as the items source for the main ListView
        /// </summary>
        private void Library_Click(Object sender, RoutedEventArgs e)
        {
            Window1.GetWindow(this).Close();

            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Multiselect = true;
            ListView LibraryList = (ListView)Application.Current.MainWindow.FindName("SongLibrary");

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var mp3Files = Directory.GetFiles(dialog.FileName, "*.mp3");

                LibraryList.Items.Clear();

                ObservableCollection<Song> songs = new ObservableCollection<Song>();

                foreach (string file in mp3Files)
                {
                    TagLib.File mp3 = TagLib.File.Create(file);
                    Song song = new Song();
                    song.Title = mp3.Tag.Title;
                    song.Artist = mp3.Tag.Performers.Length > 0 ? mp3.Tag.Performers[0] : "";
                    song.Album = mp3.Tag.Album;
                    song.Duration = TimeSpan.Parse(mp3.Properties.Duration.ToString(@"mm\:ss"));
                    song.FilePath = file;
                    songs.Add(song);
                }

                LibraryList.ItemsSource = songs;
            }
        }
        /// <summary>
        /// Closes window1 and opens playlist creator
        /// </summary>
        private void Playlist_Click(Object sender, RoutedEventArgs e)
        {
            Window1.GetWindow(this).Close();

            Window2 PlaylistCreator = new Window2();

            PlaylistCreator.ShowDialog();


        }
        /// <summary>
        /// Closes window1 and opens album creator
        /// </summary>
        private void Album_Click(Object sender, RoutedEventArgs e)
        {
            Window1.GetWindow(this).Close();

            Window2 AlbumCreator = new Window2();

            AlbumCreator.ShowDialog();


        }
    }
}
