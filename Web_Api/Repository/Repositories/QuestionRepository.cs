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
    public class QuestionRepository : IRepository<Question>
    {
        private readonly IContext context;
        public QuestionRepository(IContext context)
        {
            this.context = context;
        }

        public async Task<Question> AddItem(Question item)
        {
            await context.Questions.AddAsync(item);
            return item;
        }

        public async Task DeleteItem(int id)
        {
            Question question = await context.Questions.FirstOrDefaultAsync(x => x.Id == id);
            if (question != null)
                context.Questions.Remove(question);
        }

        public async Task<List<Question>> GetAll()
        {
            return await context.Questions.ToListAsync();
        }

        public async Task<Question> GetById(int id)
        {
            return await context.Questions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateItem(Question item)
        {
            Question question = await context.Questions.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (question != null)
            {
                question.Answers = item.Answers;
                question.IsRequired = item.IsRequired;
                question.Options = item.Options;
                question.Label = item.Label;
                question.TypeTag = item.TypeTag;
            }
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
