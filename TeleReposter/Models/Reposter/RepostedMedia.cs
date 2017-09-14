using System.Collections.Generic;
using TeleReposter.Models.Instagram;

namespace TeleReposter.Models.Reposter
{
    public class RepostedMedia
    {
        public Resource Resource { get; set; }
        public List<Item> Items { get; set; }
    }
}