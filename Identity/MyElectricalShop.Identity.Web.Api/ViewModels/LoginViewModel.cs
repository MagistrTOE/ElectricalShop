using System.ComponentModel.DataAnnotations;

namespace MyElectricalShop.Identity.Web.Api.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
        public string Error { get; set; }
        public string ReturnUrl { get; set; }
    }
}
