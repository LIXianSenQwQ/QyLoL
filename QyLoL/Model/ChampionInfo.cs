using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QyLoL.Model
{
    public class ChampionInfo
    {
        public string Id { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 头像图片地址
        /// </summary>
        public string AvatarImgUrl {  get; set; } = string.Empty;



    }
}
