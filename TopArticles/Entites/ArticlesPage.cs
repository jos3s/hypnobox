using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TopArticles.Entites
{
    public class ArticlesPage
    {
        [JsonPropertyName("page")]
        public int Page {  get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage {  get; set; }

        [JsonPropertyName("total")]
        public int Total {  get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages {  get; set; }

        [JsonPropertyName("data")]
        public List<Article> Data {  get; set; }

    }
}
