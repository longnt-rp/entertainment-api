using EntertainmentAPI.Models.Topic;
using EntertainmentAPI.Models;
using EntertainmentAPI.Requests.Topic;
using EntertainmentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EntertainmentAPI.Controllers
{
    [Route("api/topics")]
    [ApiController]
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet("index")]
        public async Task<ResponseModel<List<TopicModel>>> Index()
        {
            var data = await _topicService.GetList();
            return data;
        }

        [HttpPost("store")]
        public async Task<ResponseModel> Store([FromBody] StoreTopicRequest request)
        {
            var res = await _topicService.Store(request);
            return res;
        }

        [HttpPatch("update/{id}")]
        public async Task<ResponseModel> Update([FromBody] UpdateTopicRequest request, Guid id)
        {
            var res = await _topicService.Update(id, request);
            return res;
        }

        [HttpDelete("delete/{id}")]
        public async Task<ResponseModel> Delete(Guid id)
        {
            var res = await _topicService.Delete(id);
            return res;
        }
    }
}
