using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ProductAndCategories_2.Models{


    public class Product{
        [Key]
        public int ProductId{get;set;}
        [Required]
        public string Name{get;set;}

        [Required]
        public string Description{get;set;}

        [Required]
        public int Price{get;set;}
        public List<Association> Categories {get;set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class Category{
        public int CategoryId{get;set;}

        [Required]
        public string Name {get;set;}
        public List<Association> Products {get;set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }

    public class Association{
        public int AssociationId{get;set;} 
        public int CategoryId{get;set;}
        public int ProductId{get;set;}
        
        // Make sure they have the same name convention with the one on top 
        public Category Category{get;set;}
        public Product Product{get;set;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}