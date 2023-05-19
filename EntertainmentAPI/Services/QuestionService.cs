using EntertainmentAPI.Models.Question;
using EntertainmentAPI.Models;
using EntertainmentAPI.Requests.Question;
using EntertainmentAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntertainmentAPI.Services
{
    public interface IQuestionService
    {
        Task<ResponseModel<List<QuestionModel>>> GetList();
        Task<ResponseModel> Store(StoreQuestionRequest req);
        Task<ResponseModel> Update(Guid questionId, UpdateQuestionRequest req);
        Task<ResponseModel> Delete(Guid questionId);
    }

    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext _context;

        public QuestionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<QuestionModel>>> GetList()
        {
            List<QuestionModel> data = await _context.Questions.Include(x => x.Topic)
                .Where(x => x.IsDeleted == 0)
                .Select(x => new QuestionModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    TopicId = x.TopicId,
                    TopicName = x.Topic.Name,
                    Image = x.Image,
                    Level = x.Level,
                    Type = x.Type
                })
                .ToListAsync();

            return new ResponseModel<List<QuestionModel>>
            {
                Status = 1,
                Data = data
            };
        }

        public async Task<ResponseModel> Store(StoreQuestionRequest req)
        {
            try
            {
                var topic = await _context.Topics.AnyAsync(x => x.Id == req.TopicId && x.IsDeleted == 0);
                if (!topic)
                {
                    return new ResponseModel
                    {
                        Status = 0,
                        Message = "Chủ đề không tồn tại"
                    };
                }

                _context.Questions.Add(new Question
                {
                    TopicId = req.TopicId,
                    Content = req.Content,
                    Image = req.Image,
                    Level = req.Level,
                    Type = req.Type
                });
                await _context.SaveChangesAsync();

                return new ResponseModel
                {
                    Status = 1,
                    Message = "Tạo câu hỏi thành công"
                };
            }
            catch (Exception)
            {
                return new ResponseModel
                {
                    Status = 0,
                    Message = "Tạo câu hỏi thất bại"
                };
            }
        }

        public async Task<ResponseModel> Update(Guid questionId, UpdateQuestionRequest req)
        {
            try
            {
                var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == questionId && x.IsDeleted == 0);
                if (question == null)
                {
                    return new ResponseModel
                    {
                        Status = 0,
                        Message = "Câu hỏi không tồn tại"
                    };
                }

                question.Content = req.Content;
                question.Image = req.Image;
                question.Level = req.Level;
                question.UpdatedAt = DateTime.Now;

                _context.Questions.Update(question);
                await _context.SaveChangesAsync();

                return new ResponseModel
                {
                    Status = 1,
                    Message = "Cập nhật câu hỏi thành công"
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

        public async Task<ResponseModel> Delete(Guid questionId)
        {
            try
            {
                var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == questionId && x.IsDeleted == 0);
                if (question == null)
                {
                    return new ResponseModel
                    {
                        Status = 0,
                        Message = "Câu hỏi không tồn tại"
                    };
                }

                question.IsDeleted = 1;
                question.UpdatedAt = DateTime.Now;

                _context.Questions.Update(question);
                await _context.SaveChangesAsync();

                return new ResponseModel
                {
                    Status = 1,
                    Message = "Xóa câu hỏi thành công"
                };
            }
            catch (Exception)
            {
                return new ResponseModel
                {
                    Status = 0,
                    Message = "Xóa câu hỏi thất bại"
                };
            }
        }
    }
}
