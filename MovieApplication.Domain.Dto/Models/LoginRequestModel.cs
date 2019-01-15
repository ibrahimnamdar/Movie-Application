using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApplication.Domain.Dto.Models
{
    public class LoginRequestModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
