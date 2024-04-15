using QyLoL.Common;
using QyLoL.Model;
using QyLoL.ViewModel;
using QyLoL.Constant;
using Newtonsoft.Json.Linq;
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
using System.Collections.ObjectModel;
using Panuon.WPF.UI;

namespace QyLoL.View
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly MainPageModel viewModel = new();

        private Thread? queryChampionThread;

        private MainWindow _windowX;
        public MainPage(MainWindow windowX)
        {
            InitializeComponent();
            this._windowX = windowX;
            this.DataContext = viewModel;
            Task.Run(() => { InitChampionList(); });
        }

        private async void InitChampionList()
        {
            // 获取版本信息
            string versionsStr = await HttpUtil.GetString("https://ddragon.leagueoflegends.com/api/versions.json");
            JArray versionsJson = JArray.Parse(versionsStr);
            Constants.Versions = versionsJson[0].ToString();

            // 获取英雄信息
            string championUrl = string.Format("https://ddragon.leagueoflegends.com/cdn/{0}/data/zh_CN/champion.json", Constants.Versions);
            string championStr = await HttpUtil.GetString(championUrl);
            JObject championJson = JObject.Parse(championStr);
            JObject dataJson = championJson.Value<JObject>("data")!;
            foreach (var key in dataJson)
            {
                var item = dataJson.Value<JObject>(key.Key)!;
                ChampionInfo model = new()
                {
                    Name = item["name"]!.ToString(),
                    Key = item["key"]!.ToString(),
                    Id = item["id"]!.ToString(),
                    Title = item["title"]!.ToString(),
                };
                model.AvatarImgUrl = string.Format(Constants.AvatarImgUrl, Constants.Versions, model.Id);
                viewModel.MainChampionAllList.Add(model);
            }
            viewModel.IsShowLoading = Visibility.Collapsed;
            viewModel.IsShowChampion = Visibility.Visible;

            //获取键
            var action = (() =>
            {
                try
                {

                    foreach (var item in viewModel.MainChampionAllList)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            viewModel.MainChampionList.Add(item);
                        });
                        // 加入延迟防止卡顿
                        Thread.Sleep(50);
                    }
                }
                catch (ThreadInterruptedException)
                {
                    return;
                }
            });
            queryChampionThread = new Thread(new ThreadStart(action));
            queryChampionThread.Start();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewModel.ModelSelectIndex == 0)
            {
                viewModel.LxVis = false;
            }
            else if (viewModel.ModelSelectIndex == 1)
            {
                viewModel.LxVis = true;
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // 搜索
            var txtBox = (TextBox)sender;
            string name = txtBox.Text;

            // 如果上一个线程还在运行，取消它
            if (queryChampionThread != null && queryChampionThread.IsAlive)
            {
                queryChampionThread.Interrupt();
            }
            var action = new Action(() =>
            {
                try
                {
                    var filteredList = viewModel.MainChampionAllList.Where(item =>
                            item.Name.Contains(name) ||
                            item.Title.Contains(name)
                        ).ToList();
                    Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        viewModel.MainChampionList.Clear();
                    });

                    foreach (var item in filteredList)
                    {
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            viewModel.MainChampionList.Add(item);
                        });
                        // 加入延迟防止卡顿
                        Thread.Sleep(50);
                    }
                }
                catch (ThreadInterruptedException)
                {
                    return;
                }
            });

            queryChampionThread = new Thread(new ThreadStart(action));
            queryChampionThread.Start();
        }

        private void ListBox_ItemClick(object sender, RoutedEventArgs e)
        {
            _windowX.SwitchPage("StatDataPage");
            if (queryChampionThread != null && queryChampionThread.IsAlive)
            {
                queryChampionThread.Interrupt();
            }
        }
    }
}
