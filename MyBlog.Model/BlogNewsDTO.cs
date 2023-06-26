using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model
{
    public class BlogNewsDTO
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime Time { get; set; }
        public int BrowseCount { get; set; }
        public int LikeCount { get; set; }
        public string? TypeInfoName { get; set; }
        public string? WriterInfoName { get; set; }
    }
}
