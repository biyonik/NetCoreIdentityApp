using System.ComponentModel.DataAnnotations;

namespace NetCoreIdentityApp.WebUI.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "{0} alanı zorunlu bir alandır!")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "{0} alanı zorunlu bir alandır!")]
        [Display(Name = "E-Posta Adresi")]
        [EmailAddress(ErrorMessage = "{0} alanı geçerli bir e-posta adresi olmalıdır!")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "{0} alanı zorunlu bir alandır!")]
        [Display(Name = "Parola")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}