﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApplication.Domain.Dto.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
