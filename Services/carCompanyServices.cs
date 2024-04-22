using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebCar.DbContext;
using WebCar.Dtos;
using WebCar.Dtos.Car;
using WebCar.Models;
using WebCar.Repository;


namespace WebCar.Services
{
    public class carCompanyService : ICarCompanyService
    {
        //Tiêm để sử dụng 
        private readonly myDbContext _dbContext;
        public carCompanyService(myDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthServiceResponseDto> createCarCompanyAsync(CarCompanyDto carCompanyDto)
        {
            try
            {
                // Tạo một đối tượng CarCompany từ dữ liệu đầu vào
                var carCompany = new CarCompany
                {
                    name = carCompanyDto.ten, // Chú ý đến việc map dữ liệu từ DTO vào entity
                    logo = carCompanyDto.logo
                };

                // Thêm đối tượng CarCompany vào DbContext
                _dbContext.CarCompanies.Add(carCompany);

                // Lưu thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                return new AuthServiceResponseDto { IsSucceed = true, Message = "Tạo CarCompany thành công" };
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return new AuthServiceResponseDto { IsSucceed = false, Message = "Đã xảy ra lỗi khi tạo CarCompany", responseData = new List<string> { ex.Message } };
            }
        }
        public async Task<AuthServiceResponseDto> getCarCompanyByIdAsync(int carCompanyId)
        {
            try
            {
                var carCompany = await _dbContext.CarCompanies.FirstOrDefaultAsync(c => c.Id == carCompanyId);

                if (carCompany != null)
                {
                    // Trả về kết quả thành công nếu tìm thấy công ty xe hơi
                    return new AuthServiceResponseDto
                    {
                        IsSucceed = true,
                        Message = "Lấy thông tin CarCompany thành công",
                        responseData = carCompany.name,
                    };
                }
                else
                {
                    // Trả về thông báo lỗi nếu không tìm thấy công ty xe hơi với ID đã cho
                    return new AuthServiceResponseDto
                    {
                        IsSucceed = false,
                        Message = $"Không tìm thấy CarCompany với ID {carCompanyId}"
                    };
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return new AuthServiceResponseDto
                {
                    IsSucceed = false,
                    Message = "Đã xảy ra lỗi khi lấy thông tin CarCompany",
                    responseData = new List<string> { ex.Message }
                };
            }
        }
        public async Task<AuthServiceResponseDto> getAllCarCompanyAsync()
        {
            try
            {
                var carCompany = await _dbContext.CarCompanies.Select(n => n.name).ToListAsync();

                if (carCompany != null)
                {
                    // Trả về kết quả thành công nếu tìm thấy công ty xe hơi
                    return new AuthServiceResponseDto
                    {
                        IsSucceed = true,
                        Message = "Lấy thông tin CarCompany thành công",
                        responseData = carCompany,
                    };
                }
                else
                {
                    // Trả về thông báo lỗi nếu không tìm thấy công ty xe hơi với ID đã cho
                    return new AuthServiceResponseDto
                    {
                        IsSucceed = false,
                        Message = $"Không tìm thấy CarCompany nào"
                    };
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return new AuthServiceResponseDto
                {
                    IsSucceed = false,
                    Message = "Đã xảy ra lỗi khi lấy thông tin CarCompany",
                    responseData = new List<string> { ex.Message }
                };
            }
        }
        public async Task<AuthServiceResponseDto> updateCarCompanyAsync(int carCompanyId, CarCompanyDto carCompanyDto)
        {
            try
            {
                var existingCarCompany = await _dbContext.CarCompanies.FindAsync(carCompanyId);

                if (existingCarCompany == null)
                {
                    return new AuthServiceResponseDto
                    {
                        IsSucceed = false,
                        Message = $"Không tìm thấy CarCompany với ID {carCompanyId}"
                    };
                }

                // Update the properties of the existing car company
                existingCarCompany.name = carCompanyDto.ten;
                existingCarCompany.logo = carCompanyDto.logo;

                // Save changes to the responseDatabase
                await _dbContext.SaveChangesAsync();

                return new AuthServiceResponseDto
                {
                    IsSucceed = true,
                    Message = "Cập nhật thông tin CarCompany thành công"
                };
            }
            catch (Exception ex)
            {
                return new AuthServiceResponseDto
                {
                    IsSucceed = false,
                    Message = $"Đã xảy ra lỗi khi cập nhật thông tin CarCompany: {ex.Message}"
                };
            }
        }
        public async Task<AuthServiceResponseDto> deleteCarCompanyAsync(int carCompanyId)
        {
            try
            {
                var existingCarCompany = await _dbContext.CarCompanies.FindAsync(carCompanyId);

                if (existingCarCompany == null)
                {
                    return new AuthServiceResponseDto
                    {
                        IsSucceed = false,
                        Message = $"Không tìm thấy CarCompany với ID {carCompanyId}"
                    };
                }
                else
                {
                    _dbContext.CarCompanies.Remove(existingCarCompany);

                    // Save changes to the database
                    await _dbContext.SaveChangesAsync();

                    return new AuthServiceResponseDto
                    {
                        IsSucceed = true,
                        Message = "Xoá CarCompany thành công"
                    };
                }
            }
            catch (Exception ex)
            {
                return new AuthServiceResponseDto
                {
                    IsSucceed = false,
                    Message = $"Đã xảy ra lỗi khi xoá CarCompany: {ex.Message}"
                };
            }
        }
    }
}
