using AutoMapper;
using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        IRepository<Question> repository;
        private readonly IMapper _mapper;
        public QuestionController(IRepository<Question> repository,IMapper mapper)
        {
            this.repository = repository;
            this._mapper = mapper;
        }

        // GET: api/<QuestionController>
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<QuestionDto>> Get()
        {
            var questions = await repository.GetAll();
            return  _mapper.Map<List<QuestionDto>>(questions);
        }

        // GET api/<QuestionController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<QuestionDto>> Get(int id)
        {
           var answer = await repository.GetById(id);
            return _mapper.Map<QuestionDto>(answer);   
        }

        // POST api/<QuestionController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] QuestionDto questionDto)
        {
            var question = _mapper.Map<Question>(questionDto);
            await repository.AddItem(question);
            await repository.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<QuestionController>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put([FromBody] QuestionDto questionDto)
        {
            var question = _mapper.Map<Question>(questionDto);
            await repository.UpdateItem(question);
            await repository.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<QuestionController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            await repository.DeleteItem(id);
            await repository.SaveChangesAsync();
            return Ok();
        }
    }
}
