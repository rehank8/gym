using System;
using System.Collections.Generic;

namespace Gym.Models
{
    public partial class Appointments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? Date { get; set; }
    }
}
