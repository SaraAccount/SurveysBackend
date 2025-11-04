using AutoMapper;
using Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interface;
using Service.Services;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IRepository<User> repository;
        private readonly IMapper mapper;
        private readonly UserService _userService;

        public UserController(IRepository<User> repository, IMapper mapper, UserService userService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._userService = userService;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<IEnumerable<UserDto>> Get()
        {
            var Users = await repository.GetAll();
            return mapper.Map<List<UserDto>>(Users);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = await repository.GetById(id);
            return mapper.Map<UserDto>(user); 
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            await repository.AddItem(user);
            await repository.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put([FromBody] User user)
        {
            await repository.UpdateItem(user);
            await repository.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            await repository.DeleteItem(id);
            await repository.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] UserRegisterDto dto)
        {
            try
            {
                await _userService.Register(dto);
                return Ok("Registration successful");
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword([FromBody] EmailDto dto)
        {
            try
            {
                await _userService.ForgotPassword(dto.Email);
                return Ok("New password sent to email");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

