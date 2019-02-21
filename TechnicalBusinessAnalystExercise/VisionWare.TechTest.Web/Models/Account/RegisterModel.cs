using System.ComponentModel.DataAnnotations;

namespace VisionWare.TechTest.Web.Models.Account
{
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}