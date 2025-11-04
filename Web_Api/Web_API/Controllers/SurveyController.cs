using AutoMapper;
using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly IRepository<Survey> repository;
        private readonly IMapper mapper;

        public SurveyController(IRepository<Survey> repository,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET: api/<SurveyController>
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<SurveyDto>> Get()
        {
            var survies = await repository.GetAll();
            return mapper.Map<List<SurveyDto>>(survies);
        }

        // GET api/<SurveyController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<SurveyDto>> Get(int id)
        {
            var survey = await repository.GetById(id);
            return mapper.Map<SurveyDto>(survey);
        }

        // POST api/<SurveyController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] SurveyDto surveyDto)
        {
            var survey = mapper.Map<Survey>(surveyDto);
            await repository.AddItem(survey);
            await repository.SaveChangesAsync();
            return Ok(survey);
        }

        //[HttpPost]
        //[Authorize]
        //public async Task<ActionResult> Post([FromBody] SurveyDto surveyDto)
        //{
        //    var survey = mapper.Map<Survey>(surveyDto);

        //    // קישור כל שאלה ל־Survey הנוכחי
        //    if (survey.Questions != null)
        //    {
        //        foreach (var question in survey.Questions)
        //        {
        //            question.Survey = survey;
        //        }
        //    }

        //    await repository.AddItem(survey);
        //    await repository.SaveChangesAsync();
        //    return Ok(survey);
        //}



        //[HttpPost]
        //[Authorize]
        //public async Task<ActionResult> Post([FromBody] SurveyDto surveyDto)
        //{
        //    // ממפה את ה־DTO ל־Entity
        //    var survey = mapper.Map<Survey>(surveyDto);

        //    // קישור כל שאלה ל־Survey הנוכחי (חשוב ל־EF)
        //    if (survey.Questions != null)
        //    {
        //        foreach (var question in survey.Questions)
        //        {
        //            question.Survey = survey;
        //        }
        //    }

        //    // ממפה את רשימת Respondents לפי Id (User רק עם Id מספיק ל־EF)
        //    if (surveyDto.RespondentId != null)
        //    {
        //        survey.Respondents = surveyDto.RespondentId
        //            .Select(id => new User { Id = id })
        //            .ToList();
        //    }

        //    await repository.AddItem(survey);
        //    await repository.SaveChangesAsync();

        //    // אפשר להחזיר את ה־DTO או את האובייקט כפי שנשמר (בהתאם לצורך)
        //    var resultDto = mapper.Map<SurveyDto>(survey);
        //    return Ok(resultDto);
        //}




        // PUT api/<SurveyController>/5
        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> Put([FromBody] SurveyDto surveyDto)
        {
            var survey = mapper.Map<Survey>(surveyDto);
            await repository.UpdateItem(survey);
            await repository.SaveChangesAsync();
            return Ok(survey);
        }

        // DELETE api/<SurveyController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> Delete(int id)
        {
            await repository.DeleteItem(id);
            await repository.SaveChangesAsync();
            return Ok();
        }
    }
}
