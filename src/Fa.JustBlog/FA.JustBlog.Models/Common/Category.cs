﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Models.Common
{
    class Category
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [StringLength(255, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} is required.")]
        [StringLength(255, ErrorMessage = "The {0} must between {2} and {1} characters", MinimumLength = 3)]
        public string UrlSlug { get; set; }

        [MaxLength(500, ErrorMessage = "The {0} must between {2} and {1} characters")]
        public string Description { get; set; }
        public virtual ICollection<Post> Posts { get; set; }


    }
}
