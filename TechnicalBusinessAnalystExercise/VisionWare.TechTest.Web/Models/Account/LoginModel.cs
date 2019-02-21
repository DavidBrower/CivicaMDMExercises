using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VisionWare.TechTest.Web.Models.Account
{
    public class LoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }
    }
}