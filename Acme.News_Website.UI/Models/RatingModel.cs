using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.News_Website.UI.Models
{
    public class RatingModel
    {
        public Guid IdBlog { get; set; }
        public Guid IdUser { get; set; }

        public int RatingPoint { get; set; }
    }
}
