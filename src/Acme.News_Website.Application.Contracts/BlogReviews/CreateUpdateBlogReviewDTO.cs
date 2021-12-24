using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acme.News_Website.BlogReviews
{
    public class CreateUpdateBlogReviewDTO
    {
        public Guid IdBlog { get; set; }
        public int RatingPoint { get; set; }
    }
}
