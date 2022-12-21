using Microsoft.AspNetCore.Identity;
using Project.Models.Models.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuySite.Models.Entity
{
    [Table("User")]
    public class User : IdentityUser
    {
        [Required]
        public  override string Email { get; set; }
        ////[Required]
        ////public string Password { get; set; }
        [Required]
        public override string PasswordHash { get; set; }
        [Required]
        public string Name { get; set; }
        public string NickName { get; set; } = "暱稱";
        //public override string UserName { get; set; }
        public string Address { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public  override string Id { get; set; }
        public DateTime? Birth { get; set; }
        public override string PhoneNumber { get; set; }
        public string PicPath { get; set; } = "637843188933582087init.jpg";
        public bool Disable { get; set; } = false;
        [Required]
        public int Sex { get; set; } = 1;
        public string State { get; set; } = UserState.普通會員.ToString();
        public ShoppingCart shoppingCart { get;set; }
        //一對多
        public  ICollection<Order> Orders { get; set; }
        public virtual ICollection<OrderDetile> Order { set; get; }
    }
    public enum UserState
    {
        普通會員,
        正式會員,
        系統管理員
    }
}