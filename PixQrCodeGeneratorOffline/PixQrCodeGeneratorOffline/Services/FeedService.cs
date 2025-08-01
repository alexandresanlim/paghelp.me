﻿using PixQrCodeGeneratorOffline.Models;
using PixQrCodeGeneratorOffline.Models.PaymentMethods.Pix.Extentions;
using PixQrCodeGeneratorOffline.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PixQrCodeGeneratorOffline.Services
{
    public class FeedService : IFeedService
    {
        public async Task<IList<Feed>> Get(string feedUrl)
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

                var rss = RSSFeedData?.Where(x => x.IsValid())?.OrderByDescending(x => x.PublishDate).ToList();

                foreach (var feed in rss)
                {
                    feed.Tag = feed.GetTag();
                }

                //foreach (var item in rss)
                //{
                //    var uri = await item.Link.GetImage();

                //    if (!string.IsNullOrWhiteSpace(uri))
                //    {
                //        item.Image = new UriImageSource { Uri = new Uri(uri), CachingEnabled = false };
                //    }
                //}

                return rss;
            }
            catch (Exception)
            {
                return null;
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
                    string html = await webClient.DownloadStringTaskAsync(uri).ConfigureAwait(false);

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
            catch (Exception)
            {
                return "";
            }
        }
    }
}
