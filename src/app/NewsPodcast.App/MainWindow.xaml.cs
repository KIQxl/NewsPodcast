using NewsPodcast.App.Pages;
using NewsPodcast.Core.Interfaces;
using System.Windows;

namespace NewsPodcast.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly INoticeServices _services;
        public MainWindow(INoticeServices services)
        {
            InitializeComponent();
            _services = services;

            MainFrame.Navigate(new HomePage(_services));
        }
    }
}