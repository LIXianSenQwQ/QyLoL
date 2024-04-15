using QyLoL.View;
using QyLoL.ViewModel;
using Newtonsoft.Json.Linq;
using Panuon.WPF.UI;
using System.Windows.Controls;

namespace QyLoL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowX
    {

        private static Dictionary<string, Page> dicPage = new();
        public MainWindow()
        {
            InitializeComponent();

            InitFramePage();
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitFramePage()
        {
            dicPage.Add("MainPage", new MainPage(this));
            dicPage.Add("StatDataPage", new StatDataPage());
            MainContentFrame.Navigate(dicPage["MainPage"]);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var btn = (Button)sender;
            var page = dicPage[btn.Tag.ToString()!];
            MainContentFrame.Navigate(page);
        }

        public void SwitchPage(string pageName)
        {
            MainContentFrame.Navigate(dicPage[pageName]);
        }
    }
}