using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;

namespace GaodeAmapDemo
{
    public class GeographyHelper
    {
        public static string key = ConfigurationManager.AppSettings["key"];
        public static string host = "https://restapi.amap.com/v3/";
        /// <summary>
        /// 根据地址获取经纬度
        /// </summary>
        /// <param name="address">北京市朝阳区阜通东大街6号 </param>
        public static string GetLonAndLat(string address)
        {
            var url = $"geocode/geo?key={key}&address={address}";
            url = host + url;
            var resultContent = NetHandle.GetHtmlContent(url);
            if (resultContent.Item1 != HttpStatusCode.OK) return string.Empty;
            var result = JsonConvert.DeserializeObject<AmapGeographyResultMode>(resultContent.Item2);
            if (result.status == "1")
            {
                return result.geocodes.FirstOrDefault().location;
            }
            return string.Empty;
        }
        /// <summary>
        /// 根据经纬度获取位置 116.481488,39.990464
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        public static string GetAddress(string longitude, string latitude)
        {
            var location = $"{longitude},{latitude}";
            var url = $"geocode/regeo?key={key}&location={location}";
            url = host + url;
            var resultContent = NetHandle.GetHtmlContent(url);
            if (resultContent.Item1 != HttpStatusCode.OK) return string.Empty;
            var result = JsonConvert.DeserializeObject<ReAmapGeographyResultMode>(resultContent.Item2);
            if (result.status == "1")
            {
                return result.regeocode.formatted_address;
            }
            return string.Empty;
        }
    }

    #region AmapGeographyResultMode
    /// <summary>
    /// model
    /// </summary>
    public class AmapGeographyResultMode
    {
        /// <summary>
        /// 返回值为 0 或 1，0 表示请求失败；1 表示请求成功。
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 当 status 为 0 时，info 会返回具体错误原因，否则返回“OK”。详情可以参阅info状态表
        /// </summary>

        public string info { get; set; }

        /// <summary>
        /// 返回结果的个数。
        /// </summary>
        public string count { get; set; }

        /// <summary>
        /// 地理编码信息列表
        /// </summary>
        public List<Geocodes> geocodes { get; set; }
    }
    public class Geocodes
    {
        /// <summary>
        /// 结构化地址信息  省份＋城市＋区县＋城镇＋乡村＋街道＋门牌号码
        /// </summary>
        public string formatted_address { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string province { get; set; }

        public string city { get; set; }

        /// <summary>
        /// 城市编码 例如：010
        /// </summary>
        public string citycode { get; set; }

        /// <summary>
        /// 地址所在的区
        /// </summary>
        public string district { get; set; }

        /// <summary>
        /// 地址所在的乡镇
        /// </summary>
        //public string township { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        public string adcode { get; set; }

        /// <summary>
        /// 坐标点  经度，纬度
        /// </summary>
        public string location { get; set; }
    }
    #endregion

    #region AmapGeographyResultMode

    public class ReAmapGeographyResultMode
    {
        /// <summary>
        /// 返回值为 0 或 1，0 表示请求失败；1 表示请求成功。
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 当 status 为 0 时，info 会返回具体错误原因，否则返回“OK”。详情可以参阅info状态表
        /// </summary>

        public string info { get; set; }

        /// <summary>
        /// 逆地理编码列表
        /// </summary>
        public Regeocodes regeocode { get; set; }


    }
    public class Regeocodes
    {
        /// <summary>
        /// 结构化地址信息  省份＋城市＋区县＋城镇＋乡村＋街道＋门牌号码
        /// </summary>
        public string formatted_address { get; set; }

        public AddressComponent addressComponent { get; set; }
    }

    public class AddressComponent
    {
        /// <summary>
        /// 省
        /// </summary>
        public string province { get; set; }

        public List<string> city { get; set; }

        public string township { get; set; }
    }

    #endregion
}
