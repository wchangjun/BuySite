using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage = "格式錯誤")]
        [Display(Name = "Email格式錯誤")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(15,ErrorMessage = "{0}長度必須在 {2} ~ {1}.",MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "請再次確認密碼")]
        public string ConfirmPassword { get; set; }
        //public string UserName { get; set; }

        public string NickName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Sex { get; set; } = 1;
        public DateTime? Birth { get; set; }
    }
    public class RegisterMsg
    {
        public string ErrorMsg { get; set; }
    }
}
