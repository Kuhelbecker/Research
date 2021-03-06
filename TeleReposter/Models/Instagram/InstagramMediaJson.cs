﻿//Partly-Autogenerated "Paste JSON as Classes"
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TeleReposter.Models.Instagram
{
    public enum InstagramMediaTypes
    {
        Video, Image
    }
    public class InstagramMediaJson
    {
        public List<Item> items { get; set; }
        public bool more_available { get; set; }
        public string status { get; set; }
    }

    [DataContract]
    public class Item
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public User user { get; set; }
        [DataMember]
        public Images images { get; set; }
        [DataMember]
        public string created_time { get; set; }
        [DataMember]
        public Caption caption { get; set; }
        [DataMember]
        public bool user_has_liked { get; set; }
        [DataMember]
        public Likes likes { get; set; }
        [DataMember]
        public Comments comments { get; set; }
        [DataMember]
        public bool can_view_comments { get; set; }
        [DataMember]
        public bool can_delete_comments { get; set; }
        [DataMember(Name = "type")]
        public string typeAsString { get; set; }
        public InstagramMediaTypes type => this.typeAsString == "video" ? InstagramMediaTypes.Video : InstagramMediaTypes.Image;
        [DataMember]
        public string link { get; set; }
        [DataMember]
        public Location location { get; set; }
        [DataMember]
        public string alt_media_url { get; set; }
        [DataMember]
        public Videos videos { get; set; }
        [DataMember]
        public int video_views { get; set; }
        [DataMember]
        public List<Carousel_Media> carousel_media { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        public string full_name { get; set; }
        public string profile_picture { get; set; }
        public string username { get; set; }
    }

    public class Images
    {
        public Thumbnail thumbnail { get; set; }
        public Low_Resolution low_resolution { get; set; }
        public Standard_Resolution standard_resolution { get; set; }
    }

    public class Thumbnail
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Low_Resolution
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Standard_Resolution
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Caption
    {
        public string id { get; set; }
        public string text { get; set; }
        public string created_time { get; set; }
        public From from { get; set; }
    }

    public class From
    {
        public string id { get; set; }
        public string full_name { get; set; }
        public string profile_picture { get; set; }
        public string username { get; set; }
    }

    public class Likes
    {
        public List<Datum> data { get; set; }
        public int count { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string full_name { get; set; }
        public string profile_picture { get; set; }
        public string username { get; set; }
    }

    public class Comments
    {
        public List<Datum1> data { get; set; }
        public int count { get; set; }
    }

    public class Datum1
    {
        public string id { get; set; }
        public string text { get; set; }
        public string created_time { get; set; }
        public From1 from { get; set; }
    }

    public class From1
    {
        public string id { get; set; }
        public string full_name { get; set; }
        public string profile_picture { get; set; }
        public string username { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
    }

    public class Videos
    {
        public Standard_Resolution1 standard_resolution { get; set; }
        public Low_Bandwidth low_bandwidth { get; set; }
        public Low_Resolution1 low_resolution { get; set; }
    }

    public class Standard_Resolution1
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Low_Bandwidth
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Low_Resolution1
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Carousel_Media
    {
        public Images1 images { get; set; }
        public List<Users_In_Photo> users_in_photo { get; set; }
        public string type { get; set; }
    }

    public class Images1
    {
        public Thumbnail1 thumbnail { get; set; }
        public Low_Resolution2 low_resolution { get; set; }
        public Standard_Resolution2 standard_resolution { get; set; }
    }

    public class Thumbnail1
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Low_Resolution2
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Standard_Resolution2
    {
        public int width { get; set; }
        public int height { get; set; }
        public string url { get; set; }
    }

    public class Users_In_Photo
    {
        public User1 user { get; set; }
        public Position position { get; set; }
    }

    public class User1
    {
        public string id { get; set; }
        public string full_name { get; set; }
        public string profile_picture { get; set; }
        public string username { get; set; }
    }

    public class Position
    {
        public float x { get; set; }
        public float y { get; set; }
    }

}
