using NewsPodcast.Core.Interfaces;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NewsPodcast.App.Pages
{
    /// <summary>
    /// Interação lógica para HomePage.xam
    /// </summary>
    public partial class HomePage : Page
    {
        private readonly INoticeServices _services;
        public HomePage(INoticeServices services)
        {
            InitializeComponent();
            _services = services;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var category = btn.Tag.ToString();

            this.NavigationService.Navigate(new CategoryNotice(_services, category, false));
        }

        private async void Border_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var card = sender as Border;
            var category = card.Tag.ToString();

            this.NavigationService.Navigate(new CategoryNotice(_services, category, false));
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                TextBox txtBox = sender as TextBox;

                var noticeAbout = txtBox.Text;

                this.NavigationService.Navigate(new CategoryNotice(_services, noticeAbout, true));
            }
        }
    }
}
