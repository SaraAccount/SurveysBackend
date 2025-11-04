using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly IContext context;

        public UserRepository(IContext context)
        {
            this.context = context;
        }

        public async Task<User> AddItem(User item)
        {
            await context.Users.AddAsync(item);
            await context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetAll()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateItem(User item)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (user != null)
            {
                user.FirstName = item.FirstName;
                user.LastName = item.LastName;
                user.BornDate = item.BornDate;
                user.Gender = item.Gender;
                user.Sector = item.Sector;
                user.City = item.City;
                user.Email = item.Email;
                user.Password = item.Password;
                user.Role = item.Role;
                user.OwnSurveys = item.OwnSurveys;
                user.AnsweredSurveys = item.AnsweredSurveys;

                await context.SaveChangesAsync();
            }
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsEmailExist(string email)
        {
            return await context.Users.AllAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
