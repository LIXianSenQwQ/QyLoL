using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QyLoL.Constant
{
    public static class Constants
    {
        public static string Versions { get; set; } = string.Empty;
        public static List<string> VersionsList { get; set; } = new List<string>();

        /// <summary>
        /// 所有英雄列表
        /// https://ddragon.leagueoflegends.com/cdn/14.7.1/data/zh_CN/champion.json
        /// </summary>
        public static List<JObject> ChampionList { get; set; } = new();

        /// <summary>
        /// 头像图片路径
        /// {0} 版本
        /// {1} 英雄Id
        /// </summary>
        public static string AvatarImgUrl = "https://ddragon.leagueoflegends.com/cdn/{0}/img/champion/{1}.png";



    }
}
