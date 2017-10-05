using System;
using Newtonsoft.Json;

namespace SlothThinking
{
    public interface ILoungeReplay
    {
        int Id { get; }
        string DiskName { get; set; }
        int FileSize { get; set; }
        string FileName { get; set; }
        string ContentType { get; set; }
        DateTime CreatedAt { get; set; }
        string Path { get; set; }
        string Extension { get; set; }
    }

    public class LoungeReplay : ILoungeReplay
    {
        public int Id { get; set; }

        [JsonProperty("disk_name")]
        public string DiskName { get; set; }

        [JsonProperty("file_name")]
        public string FileName { get; set; }

        [JsonProperty("file_size")]
        public int FileSize { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("sort_order")]
        public int SortOrder { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatred_at")]
        public DateTime UpdatedAt { get; set; }

        public string Path { get; set; }

        public string Extension { get; set; }
    }
}