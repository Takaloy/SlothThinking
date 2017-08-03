using System;

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
    }

    public class LoungeReplay : ILoungeReplay
    {
        public int Id { get; set; }
        public string DiskName { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string ContentType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Field { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
    }
}