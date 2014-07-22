using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HtmlAgilityPack;
using System.Net;

namespace FragRadio_Desktop_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MediaPlayer mp = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
            startStream();
            getStreamInfo(); 
        }

        private void startStream() {
            mp.Open(new Uri("icyx://streamlb.fragradio.com:8000/live"));
            mp.Play();
        }

        private void stopStream()
        {
            mp.Stop();
        }

        private async Task getStreamInfo()
        {
            WebClient wc = new WebClient();

            while (true)
            {
                string stats = await (wc.DownloadStringTaskAsync(new Uri("http://fragradio.com/includes/stats.php")));

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(stats);

                string nowPlayingText = doc.DocumentNode.SelectSingleNode("//div[@class='audio_track_title']").InnerText.Replace("SONG TITLE:", "").Trim();
                string currentDJtext = doc.DocumentNode.SelectSingleNode("//div[@class='audio_dj_title']").InnerText.Replace("PRESENTER:", "").Trim();

                nowplaying.Content = nowPlayingText;
                currentdj.Content = currentDJtext;

                this.Title = nowPlayingText + " | " + currentDJtext + " | FragRadio";

                await (Task.Delay(5000));
            }
        }

        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartStopButton.Content == "Start")
            {
                startStream();
                StartStopButton.Content = "Stop";
            }
            else
            {
                stopStream();
                StartStopButton.Content = "Start";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Feature not working yet!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Feature not working yet!");
        }      
      

    }
}
