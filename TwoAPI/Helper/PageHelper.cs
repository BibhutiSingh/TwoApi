using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TwoAPI.Models;

namespace TwoAPI.Helper
{
    public static class PageHelper
    {
        public static List<ItemModel> ToPages(this IIncludableQueryable<Item, Category> items,int pageindex, int pagesize )
        {
            return items.Skip((pageindex - 1) * pagesize).Take(pagesize)
                .Select(x => new ItemModel()
                {
                    Id = x.Id,
                    CategoryName = x.SubCategory.Category.Name,
                    SubCategoryName = x.SubCategory.Name,
                    Name=x.Name,
                    Description= x.Description
                }).ToList();
        }
    }
}
