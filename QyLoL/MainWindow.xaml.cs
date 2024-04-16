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
            MainContentFrame.Navigate(new MainPage(this));
        }

        public void SwitchPage(Page page)
        {
            MainContentFrame.Navigate(page);
        }
    }
}