using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
