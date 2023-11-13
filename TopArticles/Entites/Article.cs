using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TopArticles.Entites
{
    public class Article
    {
        [JsonPropertyName("title")]
        public string?  Title { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("num_comments")]
        public int? NumComments { get; set; }

        [JsonPropertyName("story_id")]
        public string? StoryId { get; set; }

        [JsonPropertyName("sotry_title")]
        public string? StoryTitle { get; set; }

        [JsonPropertyName("story_url")]
        public string? StoryUrl { get; set; }

        [JsonPropertyName("parent_id")]
        public string? ParentId { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreateAt { get; set; }
    }
}
