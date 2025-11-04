//using Microsoft.EntityFrameworkCore;
//using Repository.Entities;
//using Repository.Interface;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Repository.Repositories
//{
//    public class SurveyRepository : IRepository<Survey>
//    {
//        private readonly IContext context;

//        public SurveyRepository(IContext context)
//        {
//            this.context = context;
//        }

//        public async Task<Survey> AddItem(Survey item)
//        {
//            await context.Surveys.AddAsync(item);
//            await context.SaveChangesAsync();
//            return item;
//        }

//        public async Task DeleteItem(int id)
//        {
//            var survey = await context.Surveys.FirstOrDefaultAsync(x => x.Id == id);
//            if (survey != null)
//            {
//                context.Surveys.Remove(survey);
//                await context.SaveChangesAsync();
//            }
//        }

//        public async Task<List<Survey>> GetAll()
//        {
//            return await context.Surveys.ToListAsync();
//        }

//        public async Task<Survey> GetById(int id)
//        {
//            return await context.Surveys.FirstOrDefaultAsync(x => x.Id == id);
//        }

//        public async Task UpdateItem(Survey item)
//        {
//            var survey = await context.Surveys.FirstOrDefaultAsync(x => x.Id == item.Id);
//            if (survey != null)
//            {
//                survey.Surveyor = item.Surveyor;
//                survey.Questions = item.Questions;
//                survey.Subject = item.Subject;
//                survey.DateClose = item.DateClose;
//                survey.Respondents = item.Respondents;
//                survey.MaxPeople = item.MaxPeople;

//                await context.SaveChangesAsync();
//            }
//        }

//        public async Task SaveChangesAsync()
//        {
//            await context.SaveChangesAsync();
//        }
//    }
//}



using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class SurveyRepository : IRepository<Survey>
    {
        private readonly IContext context;

        public SurveyRepository(IContext context)
        {
            this.context = context;
        }

        public async Task<Survey> AddItem(Survey item)
        {
            await context.Surveys.AddAsync(item);
            await context.SaveChangesAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var survey = await context.Surveys.FirstOrDefaultAsync(x => x.Id == id);
            if (survey != null)
            {
                context.Surveys.Remove(survey);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Survey>> GetAll()
        {
            return await context.Surveys
                .Include(s => s.Questions)          // 👈 טוען את כל השאלות
                .Include(s => s.Respondents)       // (אופציונלי – אם את צריכה גם אותם)
                .Include(s => s.Surveyor)          // (אם את צריכה גם את יוצר הסקר)
                .ToListAsync();
        }

        public async Task<Survey> GetById(int id)
        {
            return await context.Surveys
                .Include(s => s.Questions)         // 👈 גם כאן
                .Include(s => s.Respondents)
                .Include(s => s.Surveyor)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateItem(Survey item)
        {
            var survey = await context.Surveys
                .Include(s => s.Questions)
                .FirstOrDefaultAsync(x => x.Id == item.Id);

            if (survey != null)
            {
                survey.Surveyor = item.Surveyor;
                survey.Questions = item.Questions;
                survey.Subject = item.Subject;
                survey.DateClose = item.DateClose;
                survey.Respondents = item.Respondents;
                survey.MaxPeople = item.MaxPeople;

                await context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
