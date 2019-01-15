using System;
using System.Collections.Generic;
using System.Text;
using MovieApplication.Domain.Dto.Models;

namespace MovieApplication.Core.Repositories.Interfaces
{
    public interface IMovieApplicationUnitOfWork : IDisposable
    {
        void Save();
        MovieApplicationBaseRepository<User> User { get; }
        MovieApplicationBaseRepository<Movie> Movie { get; }
        MovieApplicationBaseRepository<Session> Session { get; }
    }
}
