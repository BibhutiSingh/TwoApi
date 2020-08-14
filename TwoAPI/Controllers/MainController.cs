using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TwoAPI.DB;
using TwoAPI.Helper;
using TwoAPI.Models;

namespace TwoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        private readonly int pageSize;
        private readonly TwoAPIDbContext dbContext;
        public MainController(TwoAPIDbContext twoAPIDbContext=null)
        {
            dbContext = twoAPIDbContext==null ? new TwoAPIDbContext() : twoAPIDbContext;
            pageSize = 10;
        }

        [HttpGet]
        [Route("GetItemByName")]
        public IEnumerable<ItemModel> ItemsByName(string itemname, int pageindex = 1)
        {
            var res = new List<ItemModel>();
            if (string.IsNullOrEmpty(itemname))
            {
                res = dbContext.Items
                    .Include(x => x.SubCategory)
                    .ThenInclude(y => y.Category).ToPages(pageindex, pageSize);

            }
            else
            {
                res = dbContext.Items
                    .Where(x => x.Name.ToLower() == itemname.ToLower())
                    .Include(x => x.SubCategory)
                    .ThenInclude(y => y.Category)
                    .ToPages(pageindex, pageSize);
            }

            return res;
        }

        [HttpDelete]
        [Route("DeleteByCategory")]
        public IActionResult DeleteCategory(string categoryname)
        {
            int res = 0;

            if (!string.IsNullOrEmpty(categoryname))
            {
                var category = dbContext.Categories.Where(x => x.Name.ToLower() == categoryname.ToLower()).FirstOrDefault();
                if (category != null)
                {
                    dbContext.Categories.Remove(category);
                    res = dbContext.SaveChanges();
                }
            }
            return Ok($"{res} records updated.");
        }
    }
}
