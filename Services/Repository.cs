using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testAngular.Data;

namespace testAngular.Services
{
    public class Repository : IRepository 
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);

            await _context.SaveChangesAsync();
        }
        public async Task<List<T>> SelectAll<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> SelectById<T>(long id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
