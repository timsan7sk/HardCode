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
    public class TypeController : Controller
    {

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<Models.Type>> ListAsync()
        {
            using (DataContext db = new DataContext((DbContextOptions<DataContext>)DataContext.OptionsBuilder.Options))
            {
                return await db.Types.Where(x => x.DeleteDate == null).ToListAsync();
            }
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Models.Type> GetAsync(Guid id)
        {
            using (DataContext db = new DataContext((DbContextOptions<DataContext>)DataContext.OptionsBuilder.Options))
            {
                return await db.Types.Where(x => x.Id == id && x.DeleteDate == null).SingleOrDefaultAsync();
            }
        }
        // POST api/values
        [HttpPost]
        public async void PostAsync(Models.Type value)
        {
            using (DataContext db = new DataContext((DbContextOptions<DataContext>)DataContext.OptionsBuilder.Options))
            {
                await db.Types.AddAsync(new Models.Type
                {
                    Id = Guid.NewGuid(),
                    Name = value.Name,
                    Overview = value.Overview,
                    CreateDate = DateTime.Now
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
                await db.Types.Where(x => x.Id == id).ExecuteUpdateAsync(s => s.SetProperty(p => p.DeleteDate, DateTime.Now.ToUniversalTime()));
                await db.SaveChangesAsync();
            }
        }
    }
}

