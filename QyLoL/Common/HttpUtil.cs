using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace QyLoL.Common
{
    public static class HttpUtil
    {
        public static async Task<string> GetString(string url, IDictionary<string, string> headers = null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36");
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                var result = await httpClient.GetAsync(url);
                if (result.StatusCode!=System.Net.HttpStatusCode.OK)
                {
                    return "";
                }
                result.EnsureSuccessStatusCode();
                return await result.Content.ReadAsStringAsync();
            }
        }
        public static async Task<string> GetUtf8String(string url, IDictionary<string, string> headers = null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                var result = await httpClient.GetAsync(url);
                result.EnsureSuccessStatusCode();
                return Encoding.UTF8.GetString(await result.Content.ReadAsByteArrayAsync());
            }
        }
        public static async Task<string> PostString(string url, string data, IDictionary<string, string> headers = null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                List<KeyValuePair<string, string>> body = new List<KeyValuePair<string, string>>();
                foreach (var item in data.Split('&'))
                {
                    var splits = item.Split('=');
                    body.Add(new KeyValuePair<string, string>(splits[0], splits[1]));
                }
                FormUrlEncodedContent content = new FormUrlEncodedContent(body);
                var result = await httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();
                return await result.Content.ReadAsStringAsync();
            }
        }
        public static async Task<string> PostJsonString(string url, string data, IDictionary<string, string> headers = null)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();
                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}
