using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HardCodeExercise.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HardCodeExercise.Controllers
{

    [Route("api/[controller]/[action]")]
    public class FieldController : Controller
    {
        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<Field>> ListAsync()
        {
            using (DataContext db = new DataContext((DbContextOptions<DataContext>)DataContext.OptionsBuilder.Options))
            {
                return await db.Fields.Where(x => x.DeleteDate == null).ToListAsync();
            }
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Field> GetAsync(Guid id)
        {
            using (DataContext db = new DataContext((DbContextOptions<DataContext>)DataContext.OptionsBuilder.Options))
            {
                return await db.Fields.Where(x => x.Id == id && x.DeleteDate == null).SingleOrDefaultAsync();
            }
        }
        // POST api/values
        [HttpPost]
        public async void PostAsync(Field value)
        {
            using (DataContext db = new DataContext((DbContextOptions<DataContext>)DataContext.OptionsBuilder.Options))
            {
                await db.Fields.AddAsync(new Field
                {
                    Id = Guid.NewGuid(),
                    Name = value.Name,
                    Overview = value.Overview,
                    CategoryId = value.CategoryId,
                    TypeId = value.TypeId,
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
                await db.Fields.Where(x => x.Id == id).ExecuteUpdateAsync(s => s.SetProperty(p => p.DeleteDate, DateTime.Now.ToUniversalTime()));
                await db.SaveChangesAsync();
            }
        }
    }
}

