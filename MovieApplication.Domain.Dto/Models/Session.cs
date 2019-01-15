using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApplication.Domain.Dto.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int? UserId { get; set; }
        public long? ExpiryDate { get; set; }

        public User User { get; set; }
    }
}
