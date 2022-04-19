using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AppController: BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AppController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Category>>> getCategories ()
        {

            var users = await _userRepository.GetUsersAsync();

            //AutoMapper löser all mapping mellan AppUser och MemberDto!
            // AUTOMAPPER KOMMER VARA SMART NOG ATT KÄNNA IGEN PROPERTIES SOM HAR SAMMA NAMN!:
            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

            return Ok(usersToReturn);

        }
    }
}