using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Telegram.Bot.Types;
using TeleReposter.Extensions;
using TeleReposter.Extensions.String;
using TeleReposter.Models.Instagram;
using TeleReposter.Models.Reposter;

namespace TeleReposter
{
    public class AutoReposter
    {
        private Bot Bot { get; set; }

        private MediaResourcesConfig Config { get; set; }

        private readonly string _configFilePath;

        public AutoReposter(string configFilePath)
        {
            this._configFilePath = configFilePath;
        }
        public void Repost(object state)
        {
            this.Intiallize();

            this.Bot.LogToChannel(-1001107875397, "*RepostMedia.Start*");

            var mediaItems = new List<RepostedMedia>();

            mediaItems.AddRange(this.CollectMedia());

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

        private void Intiallize()
        {
            this.Config = System.IO.File.ReadAllText(this._configFilePath).FromJsonTo<MediaResourcesConfig>();
            this.Bot = new Bot();
        }

        public List<RepostedMedia> CollectMedia()
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
                    
                    var lastReposted = this.Config.lastReposted.FirstOrDefault(lr => lr.resource == resource.name);

                    var instagramMedia = this.TryGetJson(response);

                    if (instagramMedia == null)
                    {
                        continue;
                    }

                    var items = instagramMedia.items;

                    if (items == null || items.Count == 0)
                    {
                        continue;
                    }

                    if (lastReposted == null)
                    {
                        mediaItems.Add(new RepostedMedia { Resource = resource,  Items = items.Take(this.Config.settings.contentCount).ToList()});

                        this.Config.lastReposted.Add(new LastReposted { resource = resource.name, itemId = items.First().id, dateTime = DateTime.Now });

                        this.UpdateConfig();

                        continue;
                    }

                    var lastAddedItemId = items.First().id;

                    foreach (var item in items)
                    {
                        if (item.id == lastReposted.itemId)
                        {
                            break;
                        }

                        mediaItems.Add(new RepostedMedia {Resource = resource, Items = new List<Item> {item}});
                    }

                    lastReposted.itemId = lastAddedItemId;
                    lastReposted.dateTime = DateTime.Now;
                    this.UpdateConfig();
                }
            }

            return mediaItems.DistinctBy(i=>i.Resource.name).ToList();
        }

        private InstagramMediaJson TryGetJson(string response)
        {
            try
            {
                return response.FromJsonTo<InstagramMediaJson>();
            }
            catch (Exception ex)
            {
                this.Bot.LogToChannel(-1001107875397, ex);
            }

            return null;
        }

        private void UpdateConfig()
        {
            System.IO.File.WriteAllText(this._configFilePath, JsonConvert.SerializeObject(this.Config, Formatting.Indented));
        }
    }
}
