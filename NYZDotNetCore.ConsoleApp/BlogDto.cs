using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYZDotNetCore.ConsoleApp
{
    [Table("Tbl_Blog")]
    internal class BlogDto
    {
        [Key]
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set;}
        public string BlogContent { get; set; }
    }

}
