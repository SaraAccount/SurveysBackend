using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interface;
using Repository.Repositories;
using Service.SurveyDataExtraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogicController : ControllerBase
    {
        private readonly IRepository<Survey> _surveyRepository;

        public LogicController(IRepository<Survey> surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        //// GET: api/<LogicController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<LogicController>/5

        [HttpGet("{id}")]
        public async Task<Dictionary<int, object>> Get(int id)
        {
            Survey s = await _surveyRepository.GetById(id);
            var result = CompleteSurvey.CompleteSurveyData(s);
            return result;
        }

        //// POST api/<LogicController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<LogicController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<LogicController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
