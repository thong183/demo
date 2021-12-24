using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.News_Website.UI.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public int TotalBlog { get; set; }
        public List<CategoryModel> Childs { get; set; }
        public string LinkTo { get; set; }
        public string CategoryUrl { get; set; }
    }
}
