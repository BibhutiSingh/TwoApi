using System.Collections.Generic;
using System.IO;

namespace TwoAPI.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
    }
    public class Category:BaseModel
    {
        public string Name { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
    public class SubCategory:BaseModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public ICollection<Item> Items { get; set; }
    }
    public class Item:BaseModel
    {
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public SubCategory SubCategory { get; set; }
    }

}
