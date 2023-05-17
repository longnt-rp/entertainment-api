using EntertainmentAPI.Models;
using EntertainmentAPI.Models.User;
using EntertainmentAPI.Requests.User;
using EntertainmentAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntertainmentAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public UserController(ApplicationDbContext context, IUserService userService) 
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet("index")]
        public async Task<ResponseModel<List<UserModel>>> Index()
        {
            var data = await _userService.GetList();
            return data;
        }

        [HttpPost("store")]
        public async Task<ResponseModel> Store([FromBody] StoreUserRequest request)
        {
            var res = await _userService.Store(request);
            return res;
        }

        [HttpPatch("update/{id}")]
        public async Task<ResponseModel> Update([FromBody] UpdateUserRequest request, Guid id)
        {
            var res = await _userService.Update(id, request);
            return res;
        }

        [HttpDelete("delete/{id}")]
        public async Task<ResponseModel> Delete(Guid id)
        {
            var res = await _userService.Delete(id);
            return res;
        }
    }
}
