namespace MyElectricalShop.Identity.Application.Authentication.Login
{
    public class LoginResponse
    {
        public bool _Success { get; set; }
        public string _Error { get; set; }
        public string ReturnUrl { get; set; }

        public static LoginResponse Success()
        {
            return new LoginResponse { _Success = true };
        }

        public static LoginResponse Error(string message)
        {
            return new LoginResponse { _Error = message };
        }
    }
}
