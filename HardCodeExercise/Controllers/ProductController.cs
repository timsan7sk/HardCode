using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HardCodeExercise.Models;

namespace HardCodeExercise.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller
    {
        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<object>> ListAsync()
        {
            using (DataContext db = new DataContext((DbContextOptions<DataContext>)DataContext.OptionsBuilder.Options))
            {
                var fields = new List<Field>();
                var result = new List<object>();
                foreach(var p in db.Products.Where(x => x.DeleteDate == null).ToList())
                {
                    fields = db.Fields.Where(x => x.CategoryId == p.CategoryId).ToList();
                    result.Add(new
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Overview = p.Overview,
                        CreateDate = p.CreateDate,
                        CategoryId = p.CategoryId,
                        Filds = fields
                    });
                }
                return result;
            }
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<object> GetAsync(Guid id)
        {
            using (DataContext db = new DataContext((DbContextOptions<DataContext>)DataContext.OptionsBuilder.Options))
            {
                var product = db.Products.Where(x => x.Id == id).SingleOrDefaultAsync();
                var category = db.Categories.Where(x => x.Id == product.Result.CategoryId).SingleOrDefaultAsync();
                var fields = db.Fields.Where(x => x.CategoryId == category.Result.Id).ToListAsync();
                return new 
                {
                    Id = product.Result.Id,
                    CreateDate = product.Result.CreateDate,
                    Name = product.Result.Name,
                    Overview = product.Result.Overview,
                    CategoryId = product.Result.CategoryId,
                    Fields = await fields
                };
            }
        }
        // POST api/values
        [HttpPost]
        public async void PostAsync(Product value)
        {
            using (DataContext db = new DataContext((DbContextOptions<DataContext>)DataContext.OptionsBuilder.Options))
            {
                await db.Products.AddAsync(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = value.Name,
                    Overview = value.Overview,
                    CategoryId = value.CategoryId,
                    CreateDate = DateTime.Now.ToUniversalTime()
                });
                await db.SaveChangesAsync();
            }
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async void DeleteAsync(Guid id)
        {
            using (DataContext db = new DataContext((DbContextOptions<DataContext>)DataContext.OptionsBuilder.Options))
            {
                await db.Products.Where(x => x.Id == id).ExecuteUpdateAsync(s => s.SetProperty(p => p.DeleteDate, DateTime.Now.ToUniversalTime()));
                await db.SaveChangesAsync();
            }
        }
    }
}

