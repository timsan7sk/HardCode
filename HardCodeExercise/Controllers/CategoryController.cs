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
    public class CategoryController : Controller
    {
        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<Category>> ListAsync()
        {
            using (DataContext db = new DataContext((DbContextOptions<DataContext>)DataContext.OptionsBuilder.Options))
            {
                return await db.Categories.Where(x => x.DeleteDate == null).ToListAsync();
            }
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<object> GetAsync(Guid id)
        {
            using (DataContext db = new DataContext((DbContextOptions<DataContext>)DataContext.OptionsBuilder.Options))
            {
                var category = db.Categories.Where(x => x.Id == id && x.DeleteDate == null).SingleOrDefaultAsync();
                var fields = db.Fields.Where(x => x.CategoryId == category.Result.Id).ToListAsync();
                return new 
                {
                    Id = category.Result.Id,
                    CreateDate = category.Result.CreateDate,
                    Name = category.Result.Name,
                    Overview = category.Result.Overview,
                    Fields = await fields
                };
            }
        }
        // POST api/values
        [HttpPost]
        public async void PostAsync(Category value)
        {
            using (DataContext db = new DataContext((DbContextOptions<DataContext>)DataContext.OptionsBuilder.Options))
            {
                await db.Categories.AddAsync(new Category
                {
                    Id = Guid.NewGuid(),
                    Name = value.Name,
                    Overview = value.Overview,
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
                await db.Categories.Where(x => x.Id == id).ExecuteUpdateAsync(s => s.SetProperty(p => p.DeleteDate, DateTime.Now.ToUniversalTime()));
                await db.SaveChangesAsync();
            }
        }
    }
}

