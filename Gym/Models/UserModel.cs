using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gym.Models
{
    public partial class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public bool IsActive { get; set; }
    }


    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
