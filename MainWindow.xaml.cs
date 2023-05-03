using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MooreMediaPlayer
{
    /// <summary>
    /// To use the program select "New..." from the list of buttons
    /// then select "Current Library" and select the folderpath containing your mp3s 
    /// 
    /// 05/02/2023
    /// The album, playlist, search, and sort songs features are currently
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MediaPlayer mediaPlayer=new MediaPlayer();

        public int currentSongIndex = -1;

        /// <summary>
        /// Updates song current index and plays song in previous index
        /// </summary>
       private void PlayPreviousSong()
        {
            if (currentSongIndex > 0)
            {
                currentSongIndex--;
                Song previousSong = (Song)SongLibrary.Items[currentSongIndex];
                mediaPlayer.Open(new Uri(previousSong.FilePath));
                mediaPlayer.Play();

            }
        }
        /// <summary>
        /// Updates song current index and plays song in next index
        /// </summary>
        private void PlayNextSong()
        {
            currentSongIndex++;
            Song nextSong = (Song)SongLibrary.Items[currentSongIndex];
            mediaPlayer.Open(new Uri(nextSong.FilePath));
            mediaPlayer.Play();
        }

       
        /// <summary>
        /// Event handler for updating current time and slider bar
        /// </summary>
        void Timer_Tick(object sender, EventArgs e)
        {
            SongProgress.Text = mediaPlayer.Position.ToString(@"m\:ss");
            SongSlider.Value = mediaPlayer.Position.TotalSeconds;
        }
        /// <summary>
        /// Event handler to set maximum value of slider to the length of the song when a song is opened
        /// </summary>
        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            SongSlider.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
        }
        /// <summary>
        /// Event handler to reset song length and progress text whenever a song is ended
        /// </summary>
        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            SongSlider.Value = 0;
            SongProgress.Text = "0:00";

        }


        /// <summary>
        /// Seeks new position from song slider and updates progress text
        /// </summary>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Position = TimeSpan.FromSeconds(SongSlider.Value);

            SongProgress.Text = mediaPlayer.Position.ToString(@"m\:ss");
        }
        /// <summary>
        /// Button click for "New..." button, opens Window1 
        /// </summary>
        private void New_Click(Object sender,RoutedEventArgs e)
        {
            Window1 popup=new Window1();

            popup.ShowDialog();
        }
        /// <summary>
        /// Button click for Play/Pause, pauses song and updates button to "Play", vice versa
        /// </summary>
        private void PlayPause_Click(object sender, RoutedEventArgs e)
        {
            string PlayPauseContent = PlayPause.Content as string;

            if (PlayPauseContent.Equals("Play"))
            {
                mediaPlayer.Play();

                PlayPause.Content = "Pause";
            }

            if (PlayPauseContent.Equals("Pause"))
            {
                mediaPlayer.Pause();

                PlayPause.Content = "Play";
            }



        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            PlayPreviousSong();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            PlayNextSong();
        }
        /// <summary>
        /// Updates currentSongIndex to 
        /// DoubleClick for songs in library, loads song to media player and starts timer event, sets Play/Pause to Pause while song is playing
        /// </summary>
        private void SongLibrary_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (SongLibrary.SelectedItem != null)
            {
                Song selectedSong = (Song)SongLibrary.SelectedItem;
                currentSongIndex = SongLibrary.SelectedIndex;
                
                mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
                mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
                
                mediaPlayer.Open(new Uri(selectedSong.FilePath));
                mediaPlayer.Play();

                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += new EventHandler(Timer_Tick);
                timer.Start();

                string PlayPauseContent=PlayPause.Content as string;
                if (PlayPauseContent.Equals("Play"))
                {
                    PlayPause.Content = "Pause";
                }


            }
        }  
    }
}
