using System;
using System.Collections.Generic;

namespace Gym.Models
{
    public partial class Certifications
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
