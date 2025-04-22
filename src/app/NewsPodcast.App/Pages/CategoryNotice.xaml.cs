using Google.Cloud.TextToSpeech.V1;
using NewsPodcast.Core.Interfaces;
using NewsPodcast.Core.Models;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace NewsPodcast.App.Pages
{
    /// <summary>
    /// Interação lógica para CategoryNotice.xam
    /// </summary>
    public partial class CategoryNotice : Page
    {
        private readonly INoticeServices _services;
        private bool isDraggingSlider = false;
        private bool isPlaying = false;
        private DispatcherTimer timer;

        public CategoryNotice(INoticeServices services, string category, bool byTextBox)
        {
            InitializeComponent();
            _services = services;

            LoadNews(category, byTextBox);
        }

        private async void LoadNews(string category, bool byTextBox)
        {
            NoNewsPanel.Visibility = Visibility.Collapsed;

            ResponseNotice notices;

            if (!byTextBox)
                notices = await _services.HandleNoticeByCategory(category);
            else
                notices = await _services.HandleNoticeAboutBy(category);

            NewsCardsWrapPanel.Children.Clear();

            if (!notices.IsSuccess)
            {
                NoNewsPanel.Visibility = Visibility.Visible;
                return;
            }

            foreach (var notice in notices.Notices)
            {
                var card = CreateCard(notice);
                NewsCardsWrapPanel.Children.Add(card);
            }

            ManageAudio(notices.NoticesDataAudio);
        }

        private Border CreateCard(Notice notice)
        {
            double cardWidth = this.ActualWidth * 0.3;
            double cardHeight = this.ActualHeight * 0.30;

            var card = new Border
            {
                Width = cardWidth,
                Height = cardHeight,
                Style = (Style)FindResource("CardHoverStyle"),
                Background = System.Windows.Media.Brushes.White,
                CornerRadius = new System.Windows.CornerRadius(10),
                Margin = new System.Windows.Thickness(10),
                BorderBrush = System.Windows.Media.Brushes.Gray,
                BorderThickness = new System.Windows.Thickness(1),
                Tag = notice.NoticeURL,
                Effect = new System.Windows.Media.Effects.DropShadowEffect
                {
                    Color = System.Windows.Media.Colors.Gray,
                    Direction = 320,
                    ShadowDepth = 4,
                    Opacity = 0.4,
                    BlurRadius = 8
                }
            };

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(150) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });

            // Imagem
            var image = new Image
            {
                Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(notice.ImageURL)), // Imagem da notícia
                Stretch = System.Windows.Media.Stretch.UniformToFill,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new System.Windows.Thickness(0),
            };

            Grid.SetRow(image, 0);
            grid.Children.Add(image);

            // Conteúdo
            var stackPanel = new StackPanel();
            Grid.SetRow(stackPanel, 1);

            var titleTextBlock = new TextBlock
            {
                Text = notice.Title,
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
                Margin = new System.Windows.Thickness(10, 10, 10, 5),
                MaxHeight = 50
            };

            var descriptionTextBlock = new TextBlock
            {
                Text = notice.Description,
                TextWrapping = TextWrapping.Wrap,
                Margin = new System.Windows.Thickness(10, 0, 10, 10),
                MaxHeight = 80
            };

            stackPanel.Children.Add(titleTextBlock);
            stackPanel.Children.Add(descriptionTextBlock);
            grid.Children.Add(stackPanel);

            card.Child = grid;

            card.MouseLeftButtonUp += (s, e) =>
            {
                var border = s as Border;
                var url = border?.Tag as string;
                if (!string.IsNullOrEmpty(url))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
            };

            return card;
        }

        private void ManageAudio(byte[] audio)
        {
            InitializeTimer();

            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}.mp3");
            File.WriteAllBytes(tempPath, audio);
            AudioPlayer.Source = new Uri(tempPath);
            AudioPlayer.Play();

            PlayPauseIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/pause.png", UriKind.Absolute));
            isPlaying = true;
            timer.Start();
        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isPlaying)
            {
                AudioPlayer.Play();
                timer.Start();
                PlayPauseIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/pause.png", UriKind.Absolute));
            }
            else
            {
                AudioPlayer.Pause();
                timer.Stop();
                PlayPauseIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/play.png", UriKind.Absolute));
            }
            isPlaying = !isPlaying;
        }

        private void Rewind_Click(object sender, RoutedEventArgs e)
        {
            if (AudioPlayer.NaturalDuration.HasTimeSpan)
            {
                AudioPlayer.Position = AudioPlayer.Position - TimeSpan.FromSeconds(5);
            }
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            if (AudioPlayer.NaturalDuration.HasTimeSpan)
            {
                AudioPlayer.Position = AudioPlayer.Position + TimeSpan.FromSeconds(5);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isDraggingSlider && AudioPlayer.NaturalDuration.HasTimeSpan)
            {
                AudioSlider.Value = AudioPlayer.Position.TotalSeconds;
            }
        }

        private void AudioSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isDraggingSlider)
                return;
        }

        private void AudioSlider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isDraggingSlider = true;
        }

        private void AudioSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isDraggingSlider = false;
            AudioPlayer.Position = TimeSpan.FromSeconds(AudioSlider.Value);
        }

        private void AudioPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (AudioPlayer.NaturalDuration.HasTimeSpan)
            {
                AudioSlider.Maximum = AudioPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            }
        }

        private void AudioPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            AudioPlayer.Stop();
            PlayPauseIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/play.png", UriKind.Absolute));
            timer.Stop();
            isPlaying = false;
            AudioSlider.Value = 0;
        }

        private void InitializeTimer()
        {
            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(500); // Atualiza o slider a cada 0.5s
                timer.Tick += Timer_Tick;
            }
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new HomePage(_services));
        }

        private void txtAboutBy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox txtBox = sender as TextBox;

                var noticeAbout = txtBox.Text;

                LoadNews(noticeAbout, true);
            }
        }
    }
}
