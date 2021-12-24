using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.News_Website.Categories
{
    public class CreateUpdateCategoryDTO
    {
        public string Name { get; set; }
        public Guid? IdParent { get; set; }
    }
}
