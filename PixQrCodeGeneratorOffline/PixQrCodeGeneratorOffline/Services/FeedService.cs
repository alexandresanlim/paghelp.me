using PixQrCodeGeneratorOffline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace PixQrCodeGeneratorOffline.Services
{
    public static class FeedService
    {
        public static List<Feed> Get(string feedUrl)
        {
            try
            {
                WebClient wclient = new WebClient();
                string RSSData = wclient.DownloadString(feedUrl);

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

                return RSSFeedData?.ToList();
            }
            catch (Exception)
            {
                return new List<Feed>();
            }
        }
    }
}
