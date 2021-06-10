using PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixQrCodeGeneratorOffline.Models.Validation.Services
{
    public class FeedValidationService : IFeedValidationService
    {
        public FeedValidation Create(Feed feed)
        {
            return new FeedValidation
            {
                IsValid = GetIsValid(feed),
                IsToday = GetIsToday(feed)
            };
        }

        private bool GetIsValid(Feed feed)
        {
            return feed != null && !string.IsNullOrWhiteSpace(feed?.Title) && !string.IsNullOrEmpty(feed?.Link?.AbsoluteUri);
        }

        private bool GetIsToday(Feed feed)
        {
            return GetIsValid(feed) && DateTimeOffset.Now.Date.Equals(feed.PublishDateLocal.Value.Date);
        }
    }
}
