//namespace TodoApi.Models
//{
//    public partial class TodoItem
//    {
//        public int? id { get; set; }
//        public string name { get; set; }
//    }
//}

using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public partial class TodoItem
    {
        public int? id { get; set; }
        [Required]
        public string name { get; set; }
        [Range(15, 50)]
        public int age { get; set; }
    }
}

