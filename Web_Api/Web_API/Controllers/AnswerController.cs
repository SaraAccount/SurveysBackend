using AutoMapper;
using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Repository.Interface;
using Repository.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        IRepository<Answer> repository;
        private readonly IMapper _mapper;
        public AnswerController(IRepository<Answer> repository, IMapper mapper)
        {
            this.repository = repository;
            _mapper = mapper;   
        }

        // GET: api/<AnswerController>
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<AnswerDto>> Get()
        {
            var answers = await repository.GetAll();
            return _mapper.Map<List<AnswerDto>>(answers);
        }

        // GET api/<AnswerController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AnswerDto>> Get(int id)
        {
            var answer = await repository.GetById(id);
            return _mapper.Map<AnswerDto>(answer);
        }

        // POST api/<AnswerController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] AnswerDto answerDto)
        {
            var answer = _mapper.Map<Answer>(answerDto);
            await repository.AddItem(answer);
            await repository.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<AnswerController>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put([FromBody] AnswerDto answerDto)
        {
            var answer = _mapper.Map<Answer>(answerDto);
            await repository.UpdateItem(answer);
            await repository.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<AnswerController>/5
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
