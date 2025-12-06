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


        [HttpGet("{id}")]
        public async Task<Dictionary<int, object>> Get(int id)
        {
            Survey s = await _surveyRepository.GetById(id);
            var result = CompleteSurvey.CompleteSurveyData(s);
            return result;
        }

    }
}
