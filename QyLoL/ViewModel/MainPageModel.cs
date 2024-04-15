using QyLoL.Model;
using Newtonsoft.Json.Linq;
using Panuon.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QyLoL.ViewModel
{
    public class MainPageModel : NotifyPropertyChangedBase
    {
        private Visibility isShowLoading = Visibility.Visible;
        private Visibility isShowChampion = Visibility.Collapsed;

        private ObservableCollection<ChampionInfo> mainChampionList = new();

        private int modelSelectIndex;

        private bool lxVis = false;
        public int ModelSelectIndex { get => modelSelectIndex; set => Set(ref modelSelectIndex, value); }

        public bool LxVis { get => lxVis; set => Set(ref lxVis, value); }
        public ObservableCollection<ChampionInfo> MainChampionList { get => mainChampionList; set => Set(ref mainChampionList, value); }
        public Visibility IsShowChampion { get => isShowChampion; set => Set(ref isShowChampion, value); }
        public Visibility IsShowLoading { get => isShowLoading; set => Set(ref isShowLoading, value); }

        public List<ChampionInfo> MainChampionAllList = new();

    }
}
