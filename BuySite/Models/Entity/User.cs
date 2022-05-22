using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuySite.Models.Entity
{
    [Table("User")]
    public class User
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string NickName { get; set; } = "暱稱";
        public string Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public DateTime? Birth { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<OrderDetile> Order { set; get; }
    }
}