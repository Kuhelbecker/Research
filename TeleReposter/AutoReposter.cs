﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Telegram.Bot.Types;
using TeleReposter.Extensions.String;
using TeleReposter.Models.Instagram;
using TeleReposter.Models.Reposter;
using TeleReposter.Services;

namespace TeleReposter
{
    public class AutoReposter
    {
        private Bot Bot { get;}

        private MediaResourcesConfig Config { get; set; }

        public AutoReposter(string configFilePath)
        {
            this.Config = new ConfigService().Get<MediaResourcesConfig>(configFilePath);
            this.Bot = new Bot();
        }
        public void Repost(object state)
        {
            this.Bot.LogToChannel(-1001107875397, "*RepostMedia.Start*");

            var mediaItems = this.CollectMedia();

            this.Bot.LogToChannel(-1001107875397,
                "*RepostMedia*" + Environment.NewLine +
                "Total count: " + mediaItems.Count);

            foreach (var mediaItem in mediaItems)
            {
                this.Bot.LogToChannel(-1001107875397,
                    "*RepostMedia*" + Environment.NewLine +
                    "From: " + mediaItem.Resource.name + Environment.NewLine +
                    "To channel: " + mediaItem.Resource.chatId + Environment.NewLine +
                    "Finded: " + mediaItem.Items.Count);

                foreach (var item in mediaItem.Items)
                {
                    if (item.type == InstagramMediaTypes.Video)
                    {
                        this.Bot.SendMediaToChannel(mediaItem.Resource.chatId, new FileToSend(item.videos.standard_resolution.url), InstagramMediaTypes.Video);
                    }
                    if (item.type == InstagramMediaTypes.Image)
                    {
                        this.Bot.SendMediaToChannel(mediaItem.Resource.chatId, new FileToSend(item.images.standard_resolution.url), InstagramMediaTypes.Image);
                    }
                }
                
            }
        }

        private RepostedMedia GetRepostedMedia(Resource resource, List<Item> items)
        {
            var lastReposted = this.Config.lastReposted.FirstOrDefault(lr => lr.resource == resource.name);

            if (lastReposted == null)
            {
                this.Config.lastReposted.Add(new LastReposted { resource = resource.name, itemId = items.First().id, dateTime = DateTime.Now });

                this.Config.Update();

                return new RepostedMedia { Resource = resource, Items = items.Take(this.Config.settings.contentCount).ToList() };
            }

            var repostedItems = items.TakeWhile(item => item.id != lastReposted.itemId).ToList();

            lastReposted.itemId = items.First().id;
            lastReposted.dateTime = DateTime.Now;
            this.Config.Update();

            return new RepostedMedia { Resource = resource, Items = repostedItems };
        }

        private List<RepostedMedia> CollectMedia()
        {
            var mediaItems = new List<RepostedMedia>();

            using (var wc = new WebClient())
            {
                foreach (var resource in this.Config.resources)
                {
                    var response = string.Empty;

                    try
                    {
                        response = wc.DownloadString($"https://www.instagram.com/{resource.name}/media/");
                    }
                    catch (Exception ex)
                    {
                        this.Bot.LogToChannel(-1001107875397, ex);
                        continue;
                    }

                    var instagramMedia = new InstagramMediaJson();

                    try
                    {
                        instagramMedia = response.FromJsonTo<InstagramMediaJson>();
                    }
                    catch (Exception ex)
                    {
                        this.Bot.LogToChannel(-1001107875397, ex);
                    }

                    var items = instagramMedia?.items;

                    if (items == null || items.Count == 0)
                    {
                        continue;
                    }

                    mediaItems.Add(this.GetRepostedMedia(resource, items));
                }
            }

            return mediaItems;
        }
    }
}