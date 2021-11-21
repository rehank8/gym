using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gym.Models
{
    public partial class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime EventDateTime { get; set; }
        public int EventDuration { get; set; }
        public string EventImagePath { get; set; }
    }
}
