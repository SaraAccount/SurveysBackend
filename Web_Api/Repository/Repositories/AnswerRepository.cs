using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class AnswerRepository : IRepository<Answer>
    {
        private readonly IContext context;
        public AnswerRepository(IContext context)
        {
            this.context = context;
        }

        public async Task<Answer> AddItem(Answer item)
        {
            await context.Answers.AddAsync(item);
            return item;
        }

        public async Task DeleteItem(int id)
        {
            Answer answer = await context.Answers.FirstOrDefaultAsync(x => x.Id == id);
            if (answer != null)
                context.Answers.Remove(answer);
        }

        public async Task<List<Answer>> GetAll()
        {
            return await context.Answers.ToListAsync();
        }

        public async Task<Answer> GetById(int id)
        {
            return await context.Answers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateItem(Answer item)
        {
            Answer answer = await context.Answers.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (answer != null)
            {
                answer.User = item.User;
                answer.UserId = item.UserId;
                answer.AnswerValue = item.AnswerValue;
                answer.IsAnswered = item.IsAnswered;
                answer.Question = item.Question;
                answer.QuestionId = item.QuestionId;
            }
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
