using Microsoft.EntityFrameworkCore;
using WebCar.DbContext;
using WebCar.Dtos;
using WebCar.Dtos.Car;
using WebCar.Models;
using WebCar.Repository;


namespace WebCar.Services
{
    public class carService : ICarService
    {
        //Tiêm để sử dụng 
        private readonly myDbContext _dbContext;
        public carService(myDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthServiceResponseDto> createCarAsync(CarDto carDto)
        {
            try
            {

                // Tạo một đối tượng Car từ dữ liệu đầu vào
                var car = new Car
                {
                    ten = carDto.ten, // Chú ý đến việc map dữ liệu từ DTO vào entity
                    hinh = carDto.hinh,
                    phienBan = carDto.phienBan,
                    namSanXuat = carDto.namSanXuat,
                    dungTich = carDto.dungTich,
                    hopSo = carDto.hopSo,
                    kieuDang = carDto.kieuDang,
                    tinhTrang = carDto.tinhTrang,
                    nhienLieu = carDto.nhienLieu,
                    kichThuoc = carDto.kichThuoc,
                    soGhe = carDto.soGhe,
                    gia = carDto.gia,
                    CarCompanyId = carDto.maHangXe,
                };

                // Thêm đối tượng Car vào DbContext
                _dbContext.Cars.Add(car);
                // Lưu thay đổi vào cơ sở dữ liệu
                await _dbContext.SaveChangesAsync();

                return new AuthServiceResponseDto { IsSucceed = true, Message = "Tạo Car thành công" };
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return new AuthServiceResponseDto { IsSucceed = false, Message = "Đã xảy ra lỗi khi tạo Car", responseData = new List<string> { ex.Message } };
            }
        }
        public async Task<AuthServiceResponseDto> getCarByIdAsync(int carId)
        {
            try
            {
                var car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.Id == carId);

                if (car != null)
                {
                    // Trả về kết quả thành công nếu tìm thấy công ty xe hơi
                    return new AuthServiceResponseDto
                    {
                        IsSucceed = true,
                        Message = "Lấy thông tin Car thành công",
                        responseData = car,
                    };
                }
                else
                {
                    // Trả về thông báo lỗi nếu không tìm thấy công ty xe hơi với ID đã cho
                    return new AuthServiceResponseDto
                    {
                        IsSucceed = false,
                        Message = $"Không tìm thấy Car với ID {carId}"
                    };
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return new AuthServiceResponseDto
                {
                    IsSucceed = false,
                    Message = "Đã xảy ra lỗi khi lấy thông tin Car",
                    responseData = new List<string> { ex.Message }
                };
            }
        }
        public async Task<AuthServiceResponseDto> getAllCarAsync()
        {
            try
            {
                var cars = await _dbContext.Cars.Select(car => car).ToListAsync();

                if (cars != null)
                {
                    // Trả về kết quả thành công nếu tìm thấy các xe hơi
                    return new AuthServiceResponseDto
                    {
                        IsSucceed = true,
                        Message = "Lấy thông tin Car thành công",
                        responseData = cars,
                    };
                }
                else
                {
                    // Trả về thông báo lỗi nếu không tìm thấy xe hơi nào
                    return new AuthServiceResponseDto
                    {
                        IsSucceed = false,
                        Message = $"Không tìm thấy Car nào"
                    };
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return new AuthServiceResponseDto
                {
                    IsSucceed = false,
                    Message = "Đã xảy ra lỗi khi lấy thông tin Car",
                    responseData = new List<string> { ex.Message }
                };
            }
        }

        public async Task<AuthServiceResponseDto> updateCarAsync(int carId, CarDto carDto)
        {
            try
            {
                var existingCar = await _dbContext.Cars.FindAsync(carId);

                if (existingCar == null)
                {
                    return new AuthServiceResponseDto
                    {
                        IsSucceed = false,
                        Message = $"Không tìm thấy Car với ID {carId}"
                    };
                }

                // Update the properties of the existing car 
                existingCar.ten = carDto.ten; // Chú ý đến việc map dữ liệu từ DTO vào entity
                existingCar.hinh = carDto.hinh;
                existingCar.phienBan = carDto.phienBan;
                existingCar.namSanXuat = carDto.namSanXuat;
                existingCar.dungTich = carDto.dungTich;
                existingCar.hopSo = carDto.hopSo;
                existingCar.kieuDang = carDto.kieuDang;
                existingCar.tinhTrang = carDto.tinhTrang;
                existingCar.nhienLieu = carDto.nhienLieu;
                existingCar.kichThuoc = carDto.kichThuoc;
                existingCar.soGhe = carDto.soGhe;
                existingCar.gia = carDto.gia;

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                return new AuthServiceResponseDto
                {
                    IsSucceed = true,
                    Message = "Cập nhật thông tin Car thành công"
                };
            }
            catch (Exception ex)
            {
                return new AuthServiceResponseDto
                {
                    IsSucceed = false,
                    Message = $"Đã xảy ra lỗi khi cập nhật thông tin Car: {ex.Message}"
                };
            }
        }
        public async Task<AuthServiceResponseDto> deleteCarAsync(int carId)
        {
            try
            {
                var existingCar = await _dbContext.Cars.FindAsync(carId);

                if (existingCar == null)
                {
                    return new AuthServiceResponseDto
                    {
                        IsSucceed = false,
                        Message = $"Không tìm thấy Car với ID {carId}"
                    };
                }
                else
                {
                    _dbContext.Cars.Remove(existingCar);

                    // Save changes to the database
                    await _dbContext.SaveChangesAsync();

                    return new AuthServiceResponseDto
                    {
                        IsSucceed = true,
                        Message = "Xoá Car thành công"
                    };
                }
            }
            catch (Exception ex)
            {
                return new AuthServiceResponseDto
                {
                    IsSucceed = false,
                    Message = $"Đã xảy ra lỗi khi xoá Car: {ex.Message}"
                };
            }
        }
    }
}
