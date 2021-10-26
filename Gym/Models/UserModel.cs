using System;
using System.Collections.Generic;

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
}
