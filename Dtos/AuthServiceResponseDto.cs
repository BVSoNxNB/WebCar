namespace WebCar.Dtos
{
    public class AuthServiceResponseDto
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public List<string> Data { get; set; }
    }
}
