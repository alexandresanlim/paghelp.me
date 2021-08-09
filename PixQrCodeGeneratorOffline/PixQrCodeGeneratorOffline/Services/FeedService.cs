using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PixQrCodeGeneratorOffline.Services
{
    public class FeedService : IFeedService
    {
        public async Task<List<Feed>> Get(string feedUrl)
        {
            try
            {
                WebClient wclient = new WebClient();
                string RSSData = await wclient.DownloadStringTaskAsync(feedUrl);

                var xml = XDocument.Parse(RSSData);
                //var itens = xml.Descendants("item");


                var RSSFeedData = (from x in xml.Descendants("item")
                                   select new Feed
                                   {
                                       Title = ((string)x.Element("title")),
                                       Link = new Uri(((string)x.Element("link"))),
                                       Description = ((string)x.Element("description")),
                                       PublishDate = DateTime.Parse(((string)x.Element("pubDate"))),
                                       Source = ((string)x.Element("source"))
                                   });

                return RSSFeedData?.Where(x => x.Validation.IsValid)?.Take(5)?.OrderByDescending(x => x.PublishDate)?.ToList();
            }
            catch (Exception)
            {
                return new List<Feed>();
            }
        }
    }

    public static class OpenGraphExtention
    {
        public static async Task<string> GetImage(this Uri uri)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    string html = await webClient.DownloadStringTaskAsync(uri);

                    html = html.Replace(" ", "");

                    if (string.IsNullOrEmpty(html))
                        return "";

                    var img = new Regex(@"""og:image""content=""(.*?)""")?.Match(html)?.Groups[1]?.Value;

                    if (!string.IsNullOrEmpty(img))
                        return img;

                    img = new Regex(@"og:imagecontent=""(.*?)""")?.Match(html)?.Groups[1]?.Value;

                    if (!string.IsNullOrEmpty(img))
                        return img;

                    img = new Regex(@"cse_mainimage""content=""(.*?)""")?.Match(html)?.Groups[1]?.Value;

                    if (!string.IsNullOrEmpty(img))
                        return img;

                    img = new Regex(@"itemprop=""image""content=""(.*?)""")?.Match(html)?.Groups[1]?.Value;

                    if (!string.IsNullOrEmpty(img))
                        return img;

                    return img;
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}
