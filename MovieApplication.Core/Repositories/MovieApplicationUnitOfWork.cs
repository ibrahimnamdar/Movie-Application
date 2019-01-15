using System;
using System.Collections.Generic;
using System.Text;
using MovieApplication.Core.Data;
using MovieApplication.Core.Repositories.Interfaces;
using MovieApplication.Domain.Dto.Models;

namespace MovieApplication.Core.Repositories
{
    public class MovieApplicationUnitOfWork : IMovieApplicationUnitOfWork
    {
        private bool _disposed = false;
        private readonly MovieApplicationDbContext _dbContext;
        private MovieApplicationBaseRepository<User> _userRepository;
        private MovieApplicationBaseRepository<Movie> _movieRepository;
        private MovieApplicationBaseRepository<Session> _sessionRepository;

        public MovieApplicationUnitOfWork(MovieApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public MovieApplicationBaseRepository<User> User => _userRepository ?? (_userRepository = new MovieApplicationBaseRepository<User>(_dbContext));
        public MovieApplicationBaseRepository<Movie> Movie => _movieRepository ?? (_movieRepository = new MovieApplicationBaseRepository<Movie>(_dbContext));
        public MovieApplicationBaseRepository<Session> Session => _sessionRepository ?? (_sessionRepository = new MovieApplicationBaseRepository<Session>(_dbContext));


        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }


    }
}
