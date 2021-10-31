using System;

namespace Gym.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
