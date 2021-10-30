using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Models
{
    public partial class LoginHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string IPAddress { get; set; }
        public string Action { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
