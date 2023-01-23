using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnmvc.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        
        public String Decribtion { get; set; }
        [Required]
        public String ISBN { get; set; }
        [Required]
        public String Author { get; set; }
        [Required]
        public Double ListPrice { get; set; }
        [Required]
        [Display(Name= "Price For 1-50")]
        public Double Price { get; set; }
        [Required]
        [Range(1,10000)]
        [Display(Name= "Price For 51-100")]
        public Double Price50 { get; set; }
        [Required]
        [Range(1,10000)]
        [Display(Name= "Price For 100+")]
        public Double Price100 { get; set; }
        [ValidateNever]
        public String ImageUrl { get; set; }

        [Required]
        [Display(Name= "Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [Required]
        [Display(Name= "CoverType")]
        public int CoverTypeId { get; set; }
        [ValidateNever]
        public CoverType CoverType { get; set; }


    }
}
