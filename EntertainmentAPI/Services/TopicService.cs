using EntertainmentAPI.Entities;
using EntertainmentAPI.Models;
using EntertainmentAPI.Models.Topic;
using EntertainmentAPI.Requests.Topic;
using Microsoft.EntityFrameworkCore;

namespace EntertainmentAPI.Services
{
    public interface ITopicService
    {
        Task<ResponseModel<List<TopicModel>>> GetList();
        Task<ResponseModel> Store(StoreTopicRequest req);
        Task<ResponseModel> Update(Guid topicId, UpdateTopicRequest req);
        Task<ResponseModel> Delete(Guid topicId);
    }

    public class TopicService : ITopicService
    {
        private readonly ApplicationDbContext _context;

        public TopicService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<TopicModel>>> GetList()
        {
            List<TopicModel> data = await _context.Topics.Where(x => x.IsDeleted == 0)
                .Select(x => new TopicModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();

            return new ResponseModel<List<TopicModel>>
            {
                Status = 1,
                Data = data
            };
        }

        public async Task<ResponseModel> Store(StoreTopicRequest req)
        {
            try
            {
                var checkExist = await _context.Topics.AnyAsync(x => x.Name == req.Name && x.IsDeleted == 0);
                if (checkExist)
                {
                    return new ResponseModel
                    {
                        Status = 0,
                        Message = "Tên chủ đề đã tồn tại"
                    };
                }

                _context.Topics.Add(new Topic
                {
                    Name = req.Name
                });
                await _context.SaveChangesAsync();

                return new ResponseModel
                {
                    Status = 1,
                    Message = "Tạo mới chủ đề thành công"
                };
            }
            catch (Exception)
            {
                return new ResponseModel
                {
                    Status = 0,
                    Message = "Tạo chủ đề thất bại"
                };
            }
        }

        public async Task<ResponseModel> Update(Guid topicId, UpdateTopicRequest req)
        {
            try
            {
                var topic = await _context.Topics.FirstOrDefaultAsync(x => x.Id == topicId && x.IsDeleted == 0);
                if (topic == null)
                {
                    return new ResponseModel
                    {
                        Status = 0,
                        Message = "Người dùng không tồn tại"
                    };
                }

                topic.Name = req.Name;
                topic.UpdatedAt = DateTime.Now;

                _context.Topics.Update(topic);
                await _context.SaveChangesAsync();

                return new ResponseModel
                {
                    Status = 1,
                    Message = "Cập nhật chủ đề thành công"
                };
            }
            catch (Exception)
            {
                return new ResponseModel
                {
                    Status = 0,
                    Message = "Cập nhật chủ đề thất bại"
                };
            }
        }

        public async Task<ResponseModel> Delete(Guid topicId)
        {
            try
            {
                var topic = await _context.Topics.Include(x => x.Questions).FirstOrDefaultAsync(x => x.Id == topicId && x.IsDeleted == 0);
                if (topic == null)
                {
                    return new ResponseModel
                    {
                        Status = 0,
                        Message = "Chủ đề không tồn tại"
                    };
                }

                if (topic.Questions != null)
                {
                    var topicHasQuestion = topic.Questions.Any(x => x.IsDeleted == 0);
                    if (topicHasQuestion)
                    {
                        return new ResponseModel
                        {
                            Status = 0,
                            Message = "Chủ đề đã có câu hỏi. Không được xóa!"
                        };

                    }
                }

                topic.IsDeleted = 1;
                topic.UpdatedAt = DateTime.Now;

                _context.Topics.Update(topic);
                await _context.SaveChangesAsync();

                return new ResponseModel
                {
                    Status = 1,
                    Message = "Xóa chủ đề thành công"
                };
            }
            catch (Exception)
            {
                return new ResponseModel
                {
                    Status = 0,
                    Message = "Xóa chủ đề thất bại"
                };
            }
        }
    }
}
