using System.ComponentModel.DataAnnotations;

namespace NetCoreIdentityApp.WebUI.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "{0} alanı zorunlu bir alandır!")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "{0} alanı zorunlu bir alandır!")]
        [Display(Name = "Parola")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}