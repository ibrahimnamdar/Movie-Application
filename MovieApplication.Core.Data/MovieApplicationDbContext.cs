using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MovieApplication.Domain.Dto.Models;

namespace MovieApplication.Core.Data
{
    public class MovieApplicationDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public MovieApplicationDbContext(DbContextOptions<MovieApplicationDbContext> options) : base(options)
        {

        }
    }
}
