namespace QLDT.Dtos.response
{
    public class AuthenticationRes
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public bool authenticated { get; set; }
        public bool? isActive { get; set; }
    }
}
