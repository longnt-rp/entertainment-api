using EntertainmentAPI.Entities;
using EntertainmentAPI.Models;
using EntertainmentAPI.Models.User;
using EntertainmentAPI.Requests.User;
using Microsoft.EntityFrameworkCore;

namespace EntertainmentAPI.Services
{
    public interface IUserService
    {
        Task<ResponseModel<List<UserModel>>> GetList();
        Task<ResponseModel> Store(StoreUserRequest req);
        Task<ResponseModel> Update(Guid userId, UpdateUserRequest req);
        Task<ResponseModel> Delete(Guid userId);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<List<UserModel>>> GetList()
        {
            List<UserModel> data = await _context.Users.Where(x => x.IsDeleted == 0)
                .Select(x => new UserModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Username = x.Username
                })
                .ToListAsync();

            return new ResponseModel<List<UserModel>>
            {
                Status = 1,
                Data = data
            };
        }

        public async Task<ResponseModel> Store(StoreUserRequest req)
        {
            try
            {
                var checkExist = await _context.Users.AnyAsync(x => x.Username == req.Username && x.IsDeleted == 0);
                if (checkExist)
                {
                    return new ResponseModel
                    {
                        Status = 0,
                        Message = "Username đã tồn tại"
                    };
                }

                _context.Users.Add(new User
                {
                    FullName = req.FullName, 
                    Username = req.Username,
                    Phone = req.Phone,
                    Email = req.Email
                });
                await _context.SaveChangesAsync();

                return new ResponseModel
                {
                    Status = 1,
                    Message = "Tạo mới người dùng thành công"
                };
            }
            catch (Exception)
            {
                return new ResponseModel
                {
                    Status = 0,
                    Message = "Tạo mới người dùng thất bại"
                };
            }            
        }

        public async Task<ResponseModel> Update(Guid userId, UpdateUserRequest req)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId && x.IsDeleted == 0);
                if (user == null)
                {
                    return new ResponseModel
                    {
                        Status = 0,
                        Message = "Người dùng không tồn tại"
                    };
                }

                user.FullName = req.FullName;
                user.Phone = req.Phone;
                user.Email = req.Email;
                user.UpdatedAt = DateTime.Now;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return new ResponseModel
                {
                    Status = 1,
                    Message = "Cập nhật người dùng thành công"
                };
            }
            catch (Exception)
            {
                return new ResponseModel
                {
                    Status = 0,
                    Message = "Cập nhật người dùng thất bại"
                };
            }
        }

        public async Task<ResponseModel> Delete(Guid userId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId && x.IsDeleted == 0);
                if (user == null)
                {
                    return new ResponseModel
                    {
                        Status = 0,
                        Message = "Người dùng không tồn tại"
                    };
                }

                user.IsDeleted = 1;
                user.UpdatedAt = DateTime.Now;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return new ResponseModel
                {
                    Status = 1,
                    Message = "Xóa người dùng thành công"
                };
            }
            catch (Exception)
            {
                return new ResponseModel
                {
                    Status = 0,
                    Message = "Xóa người dùng thất bại"
                };
            }
        }
    }
}
