using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreDemo.Models
{
    public class CoursePatchVM
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        [Range(1, 5)]
        public int Credits { get; set; }
    }
}
