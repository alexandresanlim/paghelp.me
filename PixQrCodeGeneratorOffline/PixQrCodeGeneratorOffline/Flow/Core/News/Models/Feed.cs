﻿using PixQrCodeGeneratorOffline.Models.Commands;
using PixQrCodeGeneratorOffline.Models.Commands.Interfaces;
using PixQrCodeGeneratorOffline.Models.Validation;
using PixQrCodeGeneratorOffline.Models.Validation.Services.Interfaces;
using PixQrCodeGeneratorOffline.Models.Viewer;
using PixQrCodeGeneratorOffline.Models.Viewer.Services.Interfaces;
using System;
using Xamarin.Forms;

namespace PixQrCodeGeneratorOffline.Models
{
    public class Feed
    {
        private readonly IFeedViewerService _feedViewerService;

        private readonly IFeedCommand _feedCommand;

        public Feed()
        {
            _feedViewerService = DependencyService.Get<IFeedViewerService>();
            _feedCommand = DependencyService.Get<IFeedCommand>();


        }

        public string Title { get; set; }

        public Uri Link { get; set; }

        public string Description { get; set; }

        public string Source { get; set; }

        public ImageSource Image { get; set; }

        public FeedTag Tag { get; set; }

        public DateTimeOffset? PublishDate { get; set; }

        public DateTimeOffset? PublishDateLocal => PublishDate?.ToLocalTime();

        public TimeSpan PublishDuration => PublishDateLocal.HasValue ? (DateTimeOffset.Now - PublishDateLocal.Value) : TimeSpan.MaxValue;

        public FeedViewer Viewer => _feedViewerService?.Create(this) ?? new FeedViewer();

        public FeedCommand Command => _feedCommand?.Create(this) ?? new FeedCommand();
    }

    public class FeedTag
    {
        public string Title { get; set; }

        public Color Color { get; set; }

        public bool IsVisible { get; set; }
    }
}
